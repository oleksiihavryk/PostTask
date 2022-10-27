using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;

/// <summary>
///     Data transfer object of user state
/// </summary>
public class UserStateDto : StateDto
{
    /// <summary>
    ///     User state identifier
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; } = string.Empty;
}