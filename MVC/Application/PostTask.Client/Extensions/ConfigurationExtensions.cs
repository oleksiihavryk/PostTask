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
}