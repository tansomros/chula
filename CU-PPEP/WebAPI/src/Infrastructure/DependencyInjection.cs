using Ardalis.GuardClauses;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Infrastructure.Data;
using CUPPEP.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CUPPEP.Infrastructure;

public static class DependencyInjection
{
    public const string ConnectionStringName = "Database";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        Guard.Against.NullOrWhiteSpace(
            connectionString,
            message: $"Connection string '{ConnectionStringName}' not found.");

        services.AddSingleton(TimeProvider.System);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<CuPpepDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, sql =>
            {
                // Keep EF Core migration history table isolated from any legacy chula_pps tables
                sql.MigrationsHistoryTable("_MigrationsHistory", "dbo");
            });
        });

        services.AddScoped<ICuPpepDbContext>(provider => provider.GetRequiredService<CuPpepDbContext>());

        return services;
    }
}
