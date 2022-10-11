namespace PostTask.Authentication.Core.DatabaseInitializer;
/// <summary>
///     Database initializer service
/// </summary>
public interface IIdentityDatabaseInitializer
{
    /// <summary>
    ///     Initialize database
    /// </summary>
    Task Initialize(InitializationMode mode);
    /// <summary>
    ///     Database initialization mode
    /// </summary>
    [Flags]
    public enum InitializationMode
    {
        /// <summary>
        ///     Initialize database with creating roles if they not exists
        /// </summary>
        Roles,
        /// <summary>
        ///     Initialize database with creating admin users if they not exists
        /// </summary>
        Admins,
        /// <summary>
        ///     Initialize database with creating default users if they not exists
        /// </summary>
        Users,
    }
}