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
        => services.AddSingleton<IIdentityDatabaseInitializer, CommonIdentityDatabaseInitializer>();
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
}