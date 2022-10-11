using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTask.Client.Shared.Constants;

namespace PostTask.Client.Controllers;
/// <summary>
///     Main application controller
/// </summary>
public sealed class HomeController : Controller
{
    /// <summary>
    /// Key from view data dictionary in views for get access to email value
    /// </summary>
    public const string EmailKey = "Email";

    /// <summary>
    ///     Site main page
    /// </summary>
    /// <returns>
    ///     View of main page
    /// </returns>
    public async Task<ViewResult> Index()
        => await Task.Run(() =>
        {
            @ViewData[EmailKey] = "https://mail.google.com/mail/?" +
                                  "view=cm&" +
                                  "fs=1&" +
                                  "to=" + "oleksii.havryk2004@gmail.com" + "&" +
                                  "su=" + "PostTask question/bug report" + "&" +
                                  $"body=" + "-" + "&";
            return View("Home_Index");
        });
    /// <summary>
    ///     Action for user login in system
    /// </summary>
    /// <returns>
    ///     Login in system and redirect to main page
    /// </returns>
    [Authorize]
    public async Task<RedirectToActionResult> Login()
        => await Task.Run(() => RedirectToAction(nameof(Index)));

    /// <summary>
    ///     Action for user logout from system
    /// </summary>
    /// <returns>
    ///     Logout from system
    /// </returns>
    [Authorize]
    public async Task<SignOutResult> Logout()
        => await Task.Run(() =>
        {
            return SignOut(
                authenticationSchemes: new[]
                {
                    AuthenticationConstants.CookieAuthenticationScheme,
                    AuthenticationConstants.OidcAuthenticationScheme
                });
        });
}