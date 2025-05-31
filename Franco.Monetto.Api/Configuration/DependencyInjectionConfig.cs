namespace Franco.Monetto.Api.Configurations;

public static class DependencyInjectionConfig
{

    public static void AddDependencyInjectionConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        // ArgumentNullException.ThrowIfNull(services);
        //
        // RegisterApiConfigurations(services, configuration);
        // RegisterApplicationConfigurations(services, configuration);
        // RegisterLocalization(services, configuration);
        //
        // NativeInjectorBootStrapper.RegisterConfigurations(services, configuration);
        // NativeInjectorBootStrapper.RegisterCustomServices(services, configuration);
        // NativeInjectorBootStrapper.RegisterWebServices(services, configuration);
        //
        // ResxConfig.RegisterMapperAndResolvers(services, configuration);
    }

    // ADD THE RESOURCES FOR TRANSLATION

    // private static void RegisterLocalization(this IServiceCollection services, ConfigurationManager configuration)
    // {
    //     services.AddLocalization();
    // }
    //
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