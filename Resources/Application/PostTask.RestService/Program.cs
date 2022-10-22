using PostTask.RestService.Core.Extensions;
using PostTask.RestService.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//Setup static data and configurations
//none

//Setup DI container
services.AddResponseFactory();

//Setup framework features
services.AddControllers()
    .Services
    .AddSwaggerWithApplicationConfiguration()
    .AddGlobalExceptionHandler();

//Build application
var app = builder.Build();
bool isDevelopment = app.Environment.IsDevelopment();

//Middleware configuration
app.UseRouting();
app.UseGlobalExceptionHandler();

if (isDevelopment)
{
    app.UseSwaggerWithApplicationConfiguration();
}

app.UseEndpointsMappedOnControllers();

app.Run();