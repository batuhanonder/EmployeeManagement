using EmployeeManagement.Api.Extensions;
using EmployeeManagement.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

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