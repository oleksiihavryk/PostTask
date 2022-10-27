namespace PostTask.RestService.Data.Interfaces;
/// <summary>
///     Minimal repository interface
/// </summary>
/// <typeparam name="TItem">
///     Containable type with id of guid type
/// </typeparam>
public interface IMinimalRepository<TItem>
{
    /// <summary>
    ///     Get all entities by user identifier
    /// </summary>
    /// <returns>
    ///     Returns all entities of current user
    /// </returns>
    Task<IEnumerable<TItem>> GetAllByUserIdentifierAsync(string userId);
    /// <summary>
    ///     Get entity by unique id of guid type
    /// </summary>
    /// <param name="id">
    ///     Entity identifier
    /// </param>
    /// <returns>
    ///     Return entity by current id
    /// </returns>
    Task<TItem> GetByIdentifierAsync(Guid id);
    /// <summary>
    ///     Check if user by passed id have an entity by passed id
    /// </summary>
    /// <param name="userId">
    ///     User identifier
    /// </param>
    /// <param name="entityId">
    ///     Entity identifier
    /// </param>
    /// <returns>
    ///     Returns true if user have an entity, otherwise returns false
    /// </returns>
    Task<bool> CheckIfUserByIdContentThisEntityById(string userId, Guid entityId);
    /// <summary>
    ///     Remove entity by unique identifier
    /// </summary>
    /// <param name="id">
    ///     Entity identifier of guid type
    /// </param>
    /// <returns>
    ///     Returns task of async operation by removing entity by identifier
    /// </returns>
    Task RemoveByIdentifierAsync(Guid id);
    /// <summary>
    ///     Add entity into repository
    /// </summary>
    /// <param name="item">
    ///     Adding entity 
    /// </param>
    /// <returns>
    ///     Returns added entity
    /// </returns>
    Task<TItem> AddAsync(TItem item);
}