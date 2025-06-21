using Franco.Core.Dto.Messaging;
using Franco.Core.Handler;
using Franco.Core.Interface;
using Franco.CrossCutting.IoC;
using Franco.Sentry.Application.Auth.Handler;
using Franco.Sentry.Application.Auth.Query;
using Franco.Sentry.Application.Auth.Service;
using Franco.Sentry.Application.Auth.Validation;
using FluentValidation;
using MediatR;

namespace Franco.Sentry.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        RegisterApiInjection(services);
        RegisterQueryInjection(services);
        RegisterCommandInjection(services);
        RegisterValidationInjection(services);
        RegisterServiceInjection(services);

        NativeInjector.RegisterWebServices(services, configuration);
        NativeInjector.RegisterCustomServices(services, configuration);
        NativeInjector.RegisterConfigurations(services, configuration);

        // ResxConfig.RegisterMapperAndResolvers(services, configuration);
    }

    public static void AddDependencyInjectionConfiguration(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
    }

    private static void RegisterApiInjection(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static void RegisterQueryInjection(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<UserLoginQuery, BaseResponse>, AuthQueryHandler>();
        // services.AddScoped<IRequestHandler<TokenQuery, RequestResponse>, LoginQueryHandler>();
    }

    private static void RegisterCommandInjection(this IServiceCollection services) {}

    private static void RegisterValidationInjection(this IServiceCollection services)
    {
        services.AddTransient<IValidator<UserLoginQuery>, UserLoginValidation>();
    }

    private static void RegisterServiceInjection(this IServiceCollection services)
    {
        services.AddScoped<JwtService>();
    }
}