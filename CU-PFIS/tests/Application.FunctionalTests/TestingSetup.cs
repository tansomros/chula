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
        return Environment.GetEnvironmentVariable("CHULA_TEST_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=chula-test;Username=postgres;Password=postgres";
    }

    internal static IServiceScopeFactory ScopeFactory { get; private set; } = null!;

    internal static TestCurrentUserService CurrentUser { get; } = new();

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
        checkCmd.Parameters.AddWithValue("db", targetDb ?? "chula-test");
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

    [SetUp]
    public void ResetUser()
    {
        CurrentUser.RunAsAdmin();
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
