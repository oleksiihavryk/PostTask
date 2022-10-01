using Microsoft.Extensions.DependencyInjection;
using PostTask.Client.Core.ErrorHandler;

namespace PostTask.Client.Core.Extensions;
/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Add error handler service into DI container
    /// </summary>
    /// <param name="services">
    ///     DI Container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        => services.AddScoped<IErrorHandler, ExceptionTypeErrorHandler>();
}