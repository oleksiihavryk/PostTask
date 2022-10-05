using System.ComponentModel.DataAnnotations;

namespace PostTask.Authentication.ViewModels;

/// <summary>
///     Register page view model
/// </summary>
public sealed class RegisterViewModel
{
    /// <summary>
    ///     Username view model
    /// </summary>
    [Required]
    [MinLength(6)]
    public string? Username { get; set; } = null;
    /// <summary>
    ///     User password view model
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = null;
    /// <summary>
    ///     User password repeat view model
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat password")]
    public string? RepeatPassword { get; set; } = null;

    /// <summary>
    ///     Page return url
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
}