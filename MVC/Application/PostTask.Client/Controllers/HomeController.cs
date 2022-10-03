using Microsoft.AspNetCore.Mvc;

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
}