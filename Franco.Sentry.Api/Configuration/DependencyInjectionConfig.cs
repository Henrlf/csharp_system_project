using Franco.Core.Handler;
using Franco.Core.Interface;
using Franco.CrossCutting.IoC;
using MediatR;

namespace Franco.Sentry.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        ApiInjections(services, configuration);
        ApplicationInjections(services, configuration);
        // RegisterLocalization(services, configuration);

        NativeInjector.RegisterWebServices(services, configuration);
        NativeInjector.RegisterCustomServices(services, configuration);
        NativeInjector.RegisterConfigurations(services, configuration);
        
        // ResxConfig.RegisterMapperAndResolvers(services, configuration);
    }

    public static void AddDependencyInjectionConfiguration(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        // RegisterLocalization(app);
    }
    
    private static void ApiInjections(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static void ApplicationInjections(this IServiceCollection services, ConfigurationManager configuration)
    {
        // services.AddScoped<IRequestHandler<TokenQuery, RequestResponse>, LoginQueryHandler>();
        // services.AddScoped<IRequestHandler<LoginCommand, RequestResponse>, LoginCommandHandler>();
        // services.AddScoped<JwtTokenService>();
    }

    // private static void RegisterLocalization(this IServiceCollection services, ConfigurationManager configuration)
    // {
    //     services.AddLocalization();
    // }
    
    // private static void RegisterLocalization(this IApplicationBuilder app)
    // {
    //     var supportedCultures = new[]
    //     {
    //         new CultureInfo("pt-br"),
    //         new CultureInfo("es-ar"),
    //         new CultureInfo("es-bo"),
    //         new CultureInfo("es-mx"),
    //         new CultureInfo("es-py"),
    //         new CultureInfo("en-us"),
    //     };
    //
    //     app.UseRequestLocalization(new RequestLocalizationOptions
    //     {
    //         SupportedCultures = supportedCultures,
    //         SupportedUICultures = supportedCultures
    //     });
    // }
}