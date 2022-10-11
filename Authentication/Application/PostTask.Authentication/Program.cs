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
services.ConfigureIdentityDatabaseInitializer(from: config);

//Setup DI Container
services.AddIdentityDatabaseInitializer(); //Add identity database initializer
services.AddUserClaimProvider();

//Setup framework features
services.AddControllersWithViews();
services.AddCommonIdentityDatabaseWithOptions(from: config);
services.AddIdentityServerWithDefaultOptions();

//Build application
var app = builder.Build();
var isDevelopment = app.Environment.IsDevelopment();

await app.InitializeCommonIdentityDatabase(); // Seeding

//Middleware configurations
app.UseRouting();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
app.UseStatusCodePages();

app.UseStaticFiles();
app.UseIdentityServer();

app.UseControllerEndpoints();

app.Run();