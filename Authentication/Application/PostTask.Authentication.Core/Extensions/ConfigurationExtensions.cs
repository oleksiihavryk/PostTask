using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Core.DatabaseInitializer;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Core.Extensions;
/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Adds identity database initializer service into DI Container
    /// </summary>
    /// <param name="services">
    ///     DI Container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddIdentityDatabaseInitializer(
        this IServiceCollection services)
        => services.AddScoped<IIdentityDatabaseInitializer, CommonIdentityDatabaseInitializer>();
    /// <summary>
    ///     Add user claim provider service into DI container
    /// </summary>
    /// <param name="services">
    ///     DI container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddUserClaimProvider(
        this IServiceCollection services)
        => services.AddScoped<IClaimProvider<User>, ContainClaimsClaimProvider>();
    /// <summary>
    ///     Configure identity database initializer options
    /// </summary>
    /// <param name="services">
    ///     Default DI Container provider
    /// </param>
    /// <param name="from">
    ///     Default application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection ConfigureIdentityDatabaseInitializer(
        this IServiceCollection services,
        IConfiguration from)
        => services.Configure<IdentityDatabaseInitializerOptions>(
            from.GetSection("IdentityDatabaseInitializer"));
}