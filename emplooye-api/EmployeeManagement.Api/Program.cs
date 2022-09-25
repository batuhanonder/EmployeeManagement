using System.Reflection;
using EmployeeManagement.Api.Extensions;
using EmployeeManagement.Api.Middleware;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

ConfigureLogs();
builder.Host.UseSerilog();

builder.Services.AddScopes(builder.Configuration);
builder.Services.AddCorsConfiguration();
builder.Services.AddMediatr();
builder.Services.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseCors("ApiCorsPolicy");  
app.UseHttpsRedirection();
app.UseAuthorization();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger").AddRedirectToHttpsPermanent();;
app.UseRewriter(option);

app.UseHealthChecks("/healthcheck");

app.MapControllers();

app.Run();

#region helper
void ConfigureLogs()
{
    // Get the environment which the application is running on
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
    // Get the configuration 
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    // Create Logger
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails() // Adds details exception
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
#endregion