using PostTask.Authentication.Core.Extensions;
using PostTask.Authentication.Data.Extensions;
using PostTask.Authentication.Extensions;
using PostTask.Authentication.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//Setup static data and configurations
config.SetupServersStaticData();
config.SetupIdentityServerStaticData();

//Setup DI Container
services.AddIdentityDatabaseInitializer(); //Add identity database initializer

//Setup framework features
services.AddMvcWithDefaultOptions();
services.AddCommonIdentityDatabaseWithOptions(from: config);
services.AddIdentityServerWithDefaultOptions();

//Build application
var app = builder.Build();

//Middleware configurations
app.UseIdentityServer();
app.Run();