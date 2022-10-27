using Microsoft.Extensions.DependencyInjection;
using PostTask.RestService.Core.ResponseFactory;

namespace PostTask.RestService.Core.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///    AddAsync response factory service into DI container 
    /// </summary>
    /// <param name="services">
    ///     DI container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddResponseFactory(this IServiceCollection services)
        => services.AddSingleton<IResponseFactory, DefaultResponseFactory>();
}