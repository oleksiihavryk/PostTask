using PostTask.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//Service features configuration

//Main application features configuration
services.AddMVcWithDefaultOptions(); //Adding mvc with setup options

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