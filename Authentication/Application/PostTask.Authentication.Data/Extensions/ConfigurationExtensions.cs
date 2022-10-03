using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostTask.Authentication.Data.Context;

namespace PostTask.Authentication.Data.Extensions;
/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Add common identity database with options from application settings
    /// </summary>
    /// <param name="services">
    ///     Service features provider
    /// </param>
    /// <param name="from">
    ///     Application settings provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddCommonIdentityDatabaseWithOptions(
        this IServiceCollection services,
        IConfiguration from)
        => services.AddDbContext<CommonIdentityDbContext>(opt =>
            opt.UseSqlServer(
                from.GetConnectionString("CommonIdentity")));
}