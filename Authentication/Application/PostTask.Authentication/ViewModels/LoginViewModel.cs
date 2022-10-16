using System.ComponentModel.DataAnnotations;

namespace PostTask.Authentication.ViewModels;
/// <summary>
///     Login page view model
/// </summary>
public sealed class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    ///     Page return url
    /// </summary>
    public string ReturnUrl { get; set; }
}