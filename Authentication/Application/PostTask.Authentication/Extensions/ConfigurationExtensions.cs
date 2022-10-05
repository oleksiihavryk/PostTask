using Microsoft.EntityFrameworkCore;
using PostTask.Authentication.Data.Context;
using PostTask.Authentication.Domain;
using PostTask.Authentication.Shared.StaticData;

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
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CommonIdentityDbContext>();

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfigurations.IdentityResources)
            .AddInMemoryApiResources(IdentityServerConfigurations.ApiResources)
            .AddInMemoryApiScopes(IdentityServerConfigurations.ApiScopes)
            .AddInMemoryClients(IdentityServerConfigurations.Clients);

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
}