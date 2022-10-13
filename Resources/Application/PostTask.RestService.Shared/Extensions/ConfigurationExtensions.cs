using Microsoft.Extensions.DependencyInjection;

namespace PostTask.RestService.Shared.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Add auto mapper into DI container
    /// </summary>
    /// <param name="services">
    ///     DI Container and service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAutoMapperWithApplicationConfiguration(
        this IServiceCollection services)
    {
        services.AddAutoMapper(opt =>
        {
        });
        return services;
    }
}