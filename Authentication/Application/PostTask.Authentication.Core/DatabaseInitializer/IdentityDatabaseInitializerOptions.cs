namespace PostTask.Authentication.Core.DatabaseInitializer;
/// <summary>
///     Identity database initializer options
/// </summary>
public class IdentityDatabaseInitializerOptions
{
    /// <summary>
    ///     All admin users which be provided into database (if they
    ///     not contained there).
    /// </summary>
    public UserData[] Admins { get; set; }
    /// <summary>
    ///     All default users which be provided into database (if they
    ///     not contained there).
    /// </summary>
    public UserData[] Users { get; set; }

    public IdentityDatabaseInitializerOptions()
    {
        Admins = Array.Empty<UserData>();
        Users = Array.Empty<UserData>();
    }
    public IdentityDatabaseInitializerOptions(
        IEnumerable<UserData> admins, 
        IEnumerable<UserData> users)
    {
        Admins = admins.ToArray();
        Users = users.ToArray();
    }
}