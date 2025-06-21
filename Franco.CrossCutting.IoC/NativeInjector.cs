using System.Text;
using System.Text.Json.Serialization;
using Franco.CrossCutting.IoC.Configuration;
using Franco.CrossCutting.IoC.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Franco.CrossCutting.IoC;

public static class NativeInjector
{
    public static void RegisterConfigurations(IServiceCollection services, IConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDatabaseSentryConfiguration(configuration);
    }
    
    public static void RegisterCustomServices(IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // services.AddMemoryCache();

        // SERVICES
        // services.AddExternalServiceConfiguration(configuration);
        // services.AddInternalServiceConfiguration(configuration);
    }
    
    public static void RegisterWebServices(IServiceCollection services, IConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        #region AddAuthentication

        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!));

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

        #endregion

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        #region AddSwaggerGen

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Quaza API",
                Version = "v1"
            });

            // TODO: REVISAR!!!
            // c.OperationFilter<AcceptLanguageHeaderOperationFilter>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        #endregion

        services.AddHttpContextAccessor();
    }
}