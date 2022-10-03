using Microsoft.Extensions.Configuration;
using PostTask.Authentication.Shared.StaticData;

namespace PostTask.Authentication.Shared.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Setup servers static data from application settings
    /// </summary>
    /// <param name="config">
    ///     Application settings provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupServersStaticData(this IConfiguration config)
    {
        var servers = config.GetSection("Servers");
        Servers.Mvc = servers.GetValue<string>("Mvc");
        return config;
    }
    /// <summary>
    ///     Setup identity server static data for configure identity server configuration
    ///     from application settings
    /// </summary>
    /// <param name="config">
    ///     Application settings provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupIdentityServerStaticData(this IConfiguration config)
    {
        var identityServerData = config.GetSection("IdentityServer");
        var mvc = identityServerData.GetSection("Mvc");
        IdentityServerStaticConfigurations.SetupClient(
            opt: builder =>
            {
                builder.SetId(mvc.GetValue<string>("ClientId"))
                    .SetSecret(mvc.GetValue<string>("ClientSecret"));
                foreach (var scope in mvc.GetSection("Scopes").Get<string[]>())
                    builder.AddScope(scope);
            },
            client: IdentityServerStaticConfigurations.IdentityClientType.Mvc);
        return config;
    }
}