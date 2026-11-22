using Chula.Application;
using Chula.Application.Common.Interfaces;
using Chula.Infrastructure;
using Chula.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NUnit.Framework;

namespace Chula.Application.FunctionalTests;

[SetUpFixture]
public sealed class TestingSetup
{
    public static string ResolveTestConnectionString()
    {
        var fromEnv = Environment.GetEnvironmentVariable("CHULA_TEST_CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(fromEnv))
        {
            return fromEnv;
        }

        // Reuse local developer PostgreSQL credentials from src/API/appsettings.Development.json if present,
        // always targeting the isolated "cupharm-test" database.
        var devSettingsPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "API", "appsettings.Development.json"));

        if (File.Exists(devSettingsPath))
        {
            var devConfig = new ConfigurationBuilder()
                .AddJsonFile(devSettingsPath, optional: true)
                .Build();

            var devConn = devConfig.GetConnectionString("Database");
            if (!string.IsNullOrWhiteSpace(devConn))
            {
                var builder = new NpgsqlConnectionStringBuilder(devConn)
                {
                    Database = "cupharm-test"
                };
                return builder.ConnectionString;
            }
        }

        return "Host=localhost;Port=5432;Database=cupharm-test;Username=postgres;Password=postgres";
    }

    internal static IServiceScopeFactory ScopeFactory { get; private set; } = null!;

    internal static TestCurrentUserService CurrentUser { get; } = new();

    internal static FakeCuppepDataSource CuppepDataSource { get; } = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        var testConnectionString = ResolveTestConnectionString();
        await EnsureTestDatabaseExistsAsync(testConnectionString);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Database"] = testConnectionString
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        services.AddSingleton<ICurrentUserService>(CurrentUser);
        services.AddSingleton<ICuppepDataSource>(CuppepDataSource);

        var provider = services.BuildServiceProvider();
        ScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ChulaDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        var initialiser = scope.ServiceProvider.GetRequiredService<ChulaDbContextInitialiser>();
        await initialiser.SeedAsync();
    }

    private static async Task EnsureTestDatabaseExistsAsync(string testConnectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(testConnectionString);
        var targetDb = builder.Database;
        builder.Database = "postgres";

        await using var conn = new NpgsqlConnection(builder.ConnectionString);
        await conn.OpenAsync();

        await using var checkCmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @db;", conn);
        checkCmd.Parameters.AddWithValue("db", targetDb ?? "cupharm-test");
        var exists = await checkCmd.ExecuteScalarAsync();
        if (exists is null)
        {
            await using var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{targetDb}\";", conn);
            await createCmd.ExecuteNonQueryAsync();
        }
    }
}

public abstract class FunctionalTestBase
{
    protected TestCurrentUserService CurrentUser => TestingSetup.CurrentUser;

    protected FakeCuppepDataSource CuppepDataSource => TestingSetup.CuppepDataSource;

    [SetUp]
    public void ResetUser()
    {
        CurrentUser.RunAsAdmin();
        CuppepDataSource.Reset();
    }

    protected static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = TestingSetup.ScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }

    protected static async Task SendAsync(IRequest request)
    {
        using var scope = TestingSetup.ScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.Send(request);
    }

    protected static async Task<TResult> WithDbContextAsync<TResult>(Func<ChulaDbContext, Task<TResult>> action)
    {
        using var scope = TestingSetup.ScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ChulaDbContext>();
        return await action(db);
    }
}
