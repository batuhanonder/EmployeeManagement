using System.Reflection;
using System.Text;
using EmployeeManagement.Application.Interfaces.Employee;
using EmployeeManagement.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
namespace EmployeeManagement.Api.Extensions;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(
            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        services.AddValidatorsFromAssembly(typeof(UpdateEmployeeCommandValidator).Assembly);
        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
            {
                Description = "Standart Authorization header using the Bearer scheme(\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
    
    public static void AddScopes(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMongoDatabase>(options => {
            var settings =  configuration.GetSection("EmployeeManagementDatabase").Get<EmployeeManagementDatabaseSettings>();
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });
        AssemblyScanner.FindValidatorsInAssembly(typeof(UpdateEmployeeRequest).Assembly)
            .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IIdentityRepository, IdentityRepository>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }

    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("ApiCorsPolicy", corsPolicyBuilder =>
        {
            options.AddDefaultPolicy(w => { w.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        }));
    }

    public static void AddMediatr(this IServiceCollection services)
    {
       services.AddMediatR(typeof(GetEmployeesQuery).Assembly);
    }

    public static void AddAuthentications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
 
    
}