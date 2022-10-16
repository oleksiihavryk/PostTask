using System.ComponentModel.DataAnnotations;
using PostTask.Authentication.Infrastructure.ModelValidators;

namespace PostTask.Authentication.ViewModels;

/// <summary>
///     Register page view model
/// </summary>
[RegisterViewModelSamePasswordsModelValidator]
public sealed class RegisterViewModel
{
    /// <summary>
    ///     Username view model
    /// </summary>
    [Required]
    [MinLength(6)]
    public string Username { get; set; } 
    /// <summary>
    ///     User password view model
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } 
    /// <summary>
    ///     User password repeat view model
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat password")]
    public string RepeatPassword { get; set; }

    /// <summary>
    ///     Page return url
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
}