using Init = PostTask.Authentication.Core.DatabaseInitializer.IIdentityDatabaseInitializer;

namespace PostTask.Authentication.Core.DatabaseInitializer;
/// <summary>
///     Implementation of identity database initializer for common database
/// </summary>
public class CommonIdentityDatabaseInitializer : IIdentityDatabaseInitializer
{
    /// <summary>
    ///     Implementation of database initialization by chosen mode
    /// </summary>
    /// <param name="mode">
    ///     Database initialization mode
    /// </param>
    public void Initialize(Init.InitializationMode mode)
    {
    }
}