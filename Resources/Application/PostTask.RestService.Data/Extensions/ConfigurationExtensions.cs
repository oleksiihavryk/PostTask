using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostTask.RestService.Shared.Constants;

namespace PostTask.RestService.Data.Extensions;
/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Add common database context into DI container and configured
    ///     database configurationFile configuration file
    /// </summary>
    /// <param name="services">
    ///     Service features and DI container provider
    /// </param>
    /// <param name="configurationFile">
    ///     Main configuration file provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddPostTaskDatabase(
        this IServiceCollection services,
        IConfiguration configurationFile)
        => services.AddDbContext<PostTaskDatabaseContext>(opt => 
            opt.UseSqlServer(
                configurationFile.GetConnectionString(
                    name: DatabaseConstants.ConnectionStringKey)));
    
}