using Microsoft.Extensions.Configuration;
using PostTask.Client.Shared.StaticData;

namespace PostTask.Client.Shared.Extensions;

/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Setup servers static data from application configuration
    /// </summary>
    /// <param name="config">
    ///     Application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupServersStaticData(this IConfiguration config)
    {
        var servers = config.GetSection("Servers");
        Servers.Authentication = servers.GetValue<string>("Authentication");
        return config;
    }
    /// <summary>
    ///     Setup oidc configuration static data for setup authentication feature
    ///     from application configuration
    /// </summary>
    /// <param name="config">
    ///     Application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupOidcConfigurationStaticData(this IConfiguration config)
    {
        var oidcConfigurations = config.GetSection("OidcConfiguration");
        OidcConfiguration.ClientId = oidcConfigurations.GetValue<string>("ClientId");
        OidcConfiguration.ClientSecret = oidcConfigurations.GetValue<string>("ClientSecret");
        OidcConfiguration.Scopes = oidcConfigurations.GetSection("Scopes").Get<string[]>();
        return config;
    }
}