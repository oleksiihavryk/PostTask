using System.ComponentModel.DataAnnotations;

namespace PostTask.Authentication.ViewModels;
/// <summary>
///     Login page view model
/// </summary>
public sealed class LoginViewModel
{
    [Required]
    public string? Username { get; set; } = null;
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = null;

    /// <summary>
    ///     Page return url
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
}