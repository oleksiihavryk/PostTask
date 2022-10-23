using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Domain;
/// <summary>
///     State is created by user
/// </summary>
public sealed class UserState : State
{
    /// <summary>
    ///     User state identifier
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; } = string.Empty;
}