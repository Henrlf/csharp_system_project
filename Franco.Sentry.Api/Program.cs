using Franco.CrossCutting.IoC.Configuration;
using Franco.Sentry.Api.Configuration;
// using Franco.CrossCutting.IoC.Configurations;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var port = Environment.GetEnvironmentVariable("APP_PORT") ?? "5186";
// var machineName = Environment.MachineName;

Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {environment}");
Console.WriteLine($"APP_PORT: {port}");
// Console.WriteLine($"MACHINE_NAME: {machineName}");

var builder = WebApplication.CreateBuilder(args);

//  SWAGGER/OpenAPI
builder.Services.AddOpenApi();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ENDPOINTS QUE PERMITEM VERIFICAR O ESTADO DE SAUDE DA APLICACAO E DOS COMPONENTES
builder.Services.AddHealthChecks();

// ADICIONA CONFIGURACOES DO PROJETO 
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.AddDependencyInjectionConfiguration();
app.AddDatabaseSentryConfiguration();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run($"http://*:{port}");

