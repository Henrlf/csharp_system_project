using Franco.Sentry.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Franco.CrossCutting.IoC.Configuration;

public static class DatabaseSentryConfig
{
    public static void AddDatabaseSentryConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // AQUI ADICIONAREMOS OS DB DE CONTEXTO
        services.AddDbContext<SentryContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("SentryConnection"), x => x.MigrationsAssembly("Franco.Sentry.Infra"));
        });

        // ADICIONA O CONTEXT
        services.AddScoped<SentryContext>();
    }

    public static void AddDatabaseSentryConfiguration(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // RUN MIGRATION
        using var scopeApp = app.ApplicationServices.CreateScope();
        var contextApp = scopeApp.ServiceProvider.GetRequiredService<SentryContext>();
        contextApp.Database.Migrate();
    }
}