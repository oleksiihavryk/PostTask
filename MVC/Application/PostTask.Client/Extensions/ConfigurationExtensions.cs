using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PostTask.Client.Shared;
using PostTask.Client.Shared.Constants;
using PostTask.Client.Shared.StaticData;

namespace PostTask.Client.Extensions;
/// <summary>
///     Application configuration
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    ///     Adds support of MVC feature with default application options
    /// </summary>
    /// <param name="services">
    ///     Default service features provider
    /// </param>
    /// <returns>
    ///     Returns MVC builder for adding extra configuration to MVC
    /// </returns>
    public static IMvcBuilder AddMvcWithDefaultOptions(this IServiceCollection services)
        => services.AddMvc(opt => opt.EnableEndpointRouting = false);
    /// <summary>
    ///     Middleware shortcut for using MVC feature in middleware chain with
    ///     default application routes
    /// </summary>
    /// <param name="app">
    ///     Default middleware chain handler provider
    /// </param>
    /// <returns>
    ///     Returns middleware chain handler provider
    /// </returns>
    public static IApplicationBuilder UseMvcWithApplicationRoutes(this IApplicationBuilder app)
        => app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: null,
                template: "{controller=Home}/{action=Index}");
        });
    /// <summary>
    ///     Middleware shortcut for using exception handler in middleware configured
    ///     by application
    /// </summary>
    /// <param name="app">
    ///     Default middleware chain handler provider
    /// </param>
    /// <returns>
    ///     Returns middleware chain handler provider
    /// </returns>
    public static IApplicationBuilder UseConfigureExceptionHandler(
        this IApplicationBuilder app)
        => app.UseExceptionHandler("/Error");
    /// <summary>
    ///     Add authentication feature with default options
    /// </summary>
    /// <param name="services">
    ///     Service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAuthenticationWithDefaultOptions(
        this IServiceCollection services)
        => services
            .AddAuthentication(opt =>
            {
                opt.DefaultScheme = AuthenticationConstants.CookieAuthenticationScheme;
                opt.DefaultAuthenticateScheme = AuthenticationConstants.OidcAuthenticationScheme;
                opt.DefaultChallengeScheme = AuthenticationConstants.OidcAuthenticationScheme;
            })
            .AddOpenIdConnect(AuthenticationConstants.OidcAuthenticationScheme, opt =>
            {
                opt.ClientId = OidcConfiguration.ClientId;
                opt.ClientSecret = OidcConfiguration.ClientSecret;

                opt.UsePkce = true;
                opt.Authority = Servers.Authentication;
                opt.ResponseType = OpenIdConnectResponseType.Code;

                opt.Scope.Clear();
                foreach (var s in OidcConfiguration.Scopes)
                    opt.Scope.Add(s);

                opt.SaveTokens = true;

                opt.GetClaimsFromUserInfoEndpoint = true;
            })
            .AddCookie(AuthenticationConstants.CookieAuthenticationScheme)
            .Services;
    /// <summary>
    ///     Add authorization service feature in system
    /// </summary>
    /// <param name="services">
    ///     Service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAuthorizationWithDefaultOptions(
        this IServiceCollection services)
        => services.AddAuthorization(opt =>
        {
            opt.AddPolicy(AuthorizationConstants.AdminOnlyPolicy, configure =>
            {
                configure.RequireAuthenticatedUser();
                configure.RequireRole(Roles.Administrator.ToString());
            });
        });
    /// <summary>
    ///     Setup route options in service
    /// </summary>
    /// <param name="services">
    ///     Application features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection SetupRouteOptions(this IServiceCollection services)
        => services.Configure<RouteOptions>(opt =>
        {
            opt.AppendTrailingSlash = true;
            opt.LowercaseUrls = true;
        });
}