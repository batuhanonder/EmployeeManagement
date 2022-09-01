var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMongoDatabase>(options => {
    var settings =  builder.Configuration.GetSection("EmployeeManagementDatabase").Get<EmployeeManagementDatabaseSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddMediatR(typeof(GetEmployeesQuery).Assembly);

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);;

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ApiCorsPolicy");  
app.UseHttpsRedirection();

app.UseAuthorization();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger").AddRedirectToHttpsPermanent();;
app.UseRewriter(option);

app.UseHealthChecks("/healthcheck");

app.MapControllers();

app.Run();