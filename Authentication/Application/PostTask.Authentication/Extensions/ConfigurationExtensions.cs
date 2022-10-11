using PostTask.Authentication.Core.DatabaseInitializer;
using PostTask.Authentication.Data.Context;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Extensions;
/// <summary>
///     Application configuration
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    ///     Adds Identity Server with default options
    /// </summary>
    /// <param name="services">
    ///     Service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddIdentityServerWithDefaultOptions(
        this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(opt =>
            {
                var pass = opt.Password;
                pass.RequireDigit = true;
                pass.RequireLowercase = false;
                pass.RequireNonAlphanumeric = false;
                pass.RequireUppercase = false;
                pass.RequiredLength = 8;
                pass.RequiredUniqueChars = 4;
            })
            .AddEntityFrameworkStores<CommonIdentityDbContext>();

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfigurations.IdentityResources)
            .AddInMemoryApiResources(IdentityServerConfigurations.ApiResources)
            .AddInMemoryApiScopes(IdentityServerConfigurations.ApiScopes)
            .AddInMemoryClients(IdentityServerConfigurations.Clients)
            .AddDeveloperSigningCredential();

        return services;
    }
    /// <summary>
    ///     Use controller endpoints middleware
    /// </summary>
    /// <param name="app">
    ///     Application middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseControllerEndpoints(
        this IApplicationBuilder app)
        => app.UseEndpoints(configure => configure.MapControllers());
    /// <summary>
    ///     Seeding default application data into service database
    /// </summary>
    /// <param name="app">
    ///     Application unit provider
    /// </param>
    /// <returns>
    ///     Returns task of async operation by seeding data into service database
    /// </returns>
    public static async Task InitializeCommonIdentityDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var initializer = scope.ServiceProvider.
            GetRequiredService<IIdentityDatabaseInitializer>();

        await initializer.Initialize(mode: 
            IIdentityDatabaseInitializer.InitializationMode.Admins | 
            IIdentityDatabaseInitializer.InitializationMode.Roles | 
            IIdentityDatabaseInitializer.InitializationMode.Users);
    }
}