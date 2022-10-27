using AutoMapper.Internal;

namespace PostTask.RestService.Data;

/// <summary>
///     Base class of all repositories
/// </summary>
public abstract class BaseRepository
{
    /// <summary>
    ///     Database context where repository store data
    /// </summary>
    protected readonly PostTaskDatabaseContext _context;

    /// <summary>
    ///     Create state repository service
    /// </summary>
    /// <param name="context">
    ///     Database context
    /// </param>
    protected BaseRepository(PostTaskDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Property updater class for updating entities
    /// </summary>
    public static class PropertyUpdater
    {
        /// <summary>
        ///     Update entity by other entity
        /// </summary>
        /// <typeparam name="T">
        ///     Type of other entity which define itself as a class
        /// </typeparam>
        /// <param name="updatingEntity">
        ///     Updating entity
        /// </param>
        /// <param name="entityData">
        ///     Entity what used for updating other entity
        /// </param>
        /// <param name="exceptParamNames">
        ///     Name of params what not updating between entities
        /// </param>
        public static void Update<T>(
            T updatingEntity, 
            T entityData,
            params string[] exceptParamNames)
            where T : class
        {
            if (!ReferenceEquals(updatingEntity, entityData))
            {
                var type = typeof(T);

                foreach (var p in type
                             .GetProperties()
                             .Where(p => p.CanRead && p.CanWrite && p.IsPublic()))
                {
                    if (!exceptParamNames.Contains(p.Name))
                    {
                        var value = p.GetValue(entityData);
                        p.SetValue(updatingEntity, value);
                    }
                }
            }
        }
    }
}