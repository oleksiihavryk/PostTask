namespace PostTask.RestService.Domain;
/// <summary>
///     State is created by user
/// </summary>
public sealed class UserState : State
{
    /// <summary>
    ///     User state identifier
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}