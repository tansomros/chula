using Ardalis.GuardClauses;
using Chula.Application.Common.Interfaces;
using Chula.Infrastructure.Data;
using Chula.Infrastructure.Data.Interceptors;
using Chula.Infrastructure.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chula.Infrastructure;

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
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptors>();

        services.AddDbContext<ChulaDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IChulaDbContext>(provider => provider.GetRequiredService<ChulaDbContext>());
        services.AddScoped<ChulaDbContextInitialiser>();

        services.Configure<CuppepIntegrationOptions>(
            configuration.GetSection(CuppepIntegrationOptions.SectionName));

        services.AddHttpClient<ICuppepDataSource, HttpCuppepDataSource>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<CuppepIntegrationOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl.TrimEnd('/'));
            client.Timeout = TimeSpan.FromSeconds(Math.Clamp(opts.TimeoutSeconds, 5, 300));
            if (!string.IsNullOrWhiteSpace(opts.ApiKey))
            {
                client.DefaultRequestHeaders.Remove("X-Api-Key");
                client.DefaultRequestHeaders.Add("X-Api-Key", opts.ApiKey);
            }
        }).ConfigurePrimaryHttpMessageHandler(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<CuppepIntegrationOptions>>().Value;
            var handler = new HttpClientHandler();
            if (opts.AllowUntrustedDevCertificate)
            {
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            }

            return handler;
        });

        return services;
    }
}
