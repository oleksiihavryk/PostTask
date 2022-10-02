using PostTask.Client.Core.Extensions;
using PostTask.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//Setup static data and configurations
//none

//Setup DI container
services.AddErrorHandler();

//Setup framework features
services.AddMvcWithDefaultOptions(); //Adding mvc with setup options

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

app.UseMvcWithApplicationRoutes();


app.Run(); // Run application with configured middleware