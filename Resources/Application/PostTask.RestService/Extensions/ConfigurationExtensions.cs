using Microsoft.OpenApi.Models;
using PostTask.RestService.Middleware;

namespace PostTask.RestService.Extensions;
/// <summary>
///     Application configuration
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    ///     Use endpoints middleware mapped on controllers by default
    /// </summary>
    /// <param name="app">
    ///     Middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    internal static IApplicationBuilder UseEndpointsMappedOnControllers(this IApplicationBuilder app)
        => app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
        });
    /// <summary>
    ///     Add swagger services and features into service with application
    ///     configuration
    /// </summary>
    /// <param name="services">
    ///     Service features provider 
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    internal static IServiceCollection AddSwaggerWithApplicationConfiguration(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "PostTask documentation",
                Title = "PostTask | Restful API",
                Version = "v1"
            });
        });
        services.AddEndpointsApiExplorer();

        return services;
    }
    /// <summary>
    ///     Use swagger middleware with application configuration
    /// </summary>
    /// <param name="app">
    ///     Middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    internal static IApplicationBuilder UseSwaggerWithApplicationConfiguration(
        this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.RoutePrefix = "swagger";
            opt.DocumentTitle = "PostTask | Rest API Documentation";
        });

        return app;
    }
    /// <summary>
    ///     Add global exception handler middleware into service features collection
    /// </summary>
    /// <param name="services">
    ///     Service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    internal static IServiceCollection AddGlobalExceptionHandler(
        this IServiceCollection services)
        => services.AddScoped<GlobalExceptionHandlerMiddleware>();
    /// <summary>
    ///     Add global exception handler feature into middleware chain
    /// </summary>
    /// <param name="app">
    ///     Middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    internal static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}