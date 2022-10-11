namespace PostTask.Authentication.Core.DatabaseInitializer;
/// <summary>
///     User data model for application settings
/// </summary>
public class UserData
{
    /// <summary>
    ///     User name model
    /// </summary>
    public string Username { get; set; } = string.Empty;
    /// <summary>
    ///     User password model
    /// </summary>
    public string Password { get; set; } = string.Empty;
}