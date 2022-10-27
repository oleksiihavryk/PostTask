namespace PostTask.RestService.Data.Interfaces;
/// <summary>
///     Default repository service interface
/// </summary>
/// <typeparam name="TItem">
///     Type of containable item in repository
/// </typeparam>
public interface IRepository<TItem> : IMinimalRepository<TItem>
{
    /// <summary>
    ///     Update entity in repository by entity identifier and entity data
    /// </summary>
    /// <param name="item">
    ///     Updating entity
    /// </param>
    /// <returns>
    ///     Returns updated entity
    /// </returns>
    Task<TItem> UpdateAsync(TItem item);
}