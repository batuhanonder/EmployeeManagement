

using System.Reflection;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EmployeeManagement.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class ServiceExtension
{

   
    public static void AddServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(
            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }
    public static void AddScopes(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMongoDatabase>(options => {
            var settings =  configuration.GetSection("EmployeeManagementDatabase").Get<EmployeeManagementDatabaseSettings>();
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });
        
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IValidator<Employee>, EmployeeValidator>();
    }

    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("ApiCorsPolicy", corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        }));
    }

    public static void AddMediatr(this IServiceCollection services)
    {
       services.AddMediatR(typeof(GetEmployeesQuery).Assembly);
    }
    
    
    
   
 }