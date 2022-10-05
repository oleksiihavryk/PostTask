using IdentityModel;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PostTask.Authentication.Shared.StaticData;

namespace PostTask.Authentication;
/// <summary>
///     Static server configuration
/// </summary>
internal static class IdentityServerConfigurations
{
    /// <summary>
    ///     All clients in identity server
    /// </summary>
    public static List<Client> Clients =>
        new()
        {
            new Client()
            {
                ClientId = IdentityServerStaticConfigurations.Mvc.Id,
                ClientSecrets =
                {
                    new Secret(value: IdentityServerStaticConfigurations.
                        Mvc.
                        Secret.
                        ToSha256())
                },

                AllowedGrantTypes = { OpenIdConnectGrantTypes.AuthorizationCode },

                AllowedScopes = IdentityServerStaticConfigurations.Mvc.Scopes,

                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                AlwaysIncludeUserClaimsInIdToken = true,

                RedirectUris =
                {
                    Servers.Mvc + "/signin-oidc",
                },
                PostLogoutRedirectUris =
                {
                    Servers.Mvc + "/signout-callback-oidc"
                }
            }
        };
    /// <summary>
    ///     All possible user identity resources which provides with jwt
    /// </summary>
    public static List<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResource(
                name: OidcConstants.StandardScopes.Profile, 
                userClaims: new List<string>()
                {
                    JwtClaimTypes.PreferredUserName
                })
        };
    /// <summary>
    ///     All api that can do authentication on current server
    /// </summary>
    public static List<ApiResource> ApiResources =>
        new List<ApiResource>()
        {
        };
    /// <summary>
    ///     All api scopes
    /// </summary>
    public static List<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
        };
}