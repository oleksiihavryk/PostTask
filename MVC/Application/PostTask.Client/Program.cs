using PostTask.Client.Core.Extensions;
using PostTask.Client.Extensions;
using PostTask.Client.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//Setup static data and configurations
services.SetupRouteOptions();
config.SetupOidcConfigurationStaticData();
config.SetupServersStaticData();

//Setup DI container
services.AddErrorHandler();

//Setup framework features
services.AddMvcWithDefaultOptions(); //Adding mvc with setup options
services.AddAuthenticationWithDefaultOptions();
services.AddAuthorizationWithDefaultOptions();

//Build application
var app = builder.Build();
var env = app.Environment;

//Middleware configuration

app.UseRouting();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage()
        .UseStatusCodePages();
}
else
{
    app.UseConfigureExceptionHandler();
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvcWithApplicationRoutes();

app.Run(); // Run application with configured middleware