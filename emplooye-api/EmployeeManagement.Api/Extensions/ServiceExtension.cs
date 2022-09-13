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
        services.AddSwaggerGen();
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
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
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