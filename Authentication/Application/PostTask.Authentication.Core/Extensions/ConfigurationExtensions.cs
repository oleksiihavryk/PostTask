using Microsoft.Extensions.DependencyInjection;
using PostTask.Authentication.Core.DatabaseInitializer;

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
}