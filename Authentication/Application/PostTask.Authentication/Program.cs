var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//Setup static data and configurations
//none

//Setup DI Container
//none

//Setup framework features
//none

//Build application
var app = builder.Build();

//Middleware configurations
app.Run();