using Microsoft.AspNetCore.Mvc;

namespace PostTask.Client.Controllers;
/// <summary>
///     Main application controller
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    ///     Site main page
    /// </summary>
    /// <returns>
    ///     View of main page
    /// </returns>
    public ViewResult Index()
    {
        @ViewBag.Email = "https://mail.google.com/mail/?" +
                         "view=cm&" +
                         "fs=1&" +
                         "to=" + "oleksii.havryk2004@gmail.com" + "&" +
                         "su=" + "PostTask question/bug report" + "&" +
                         $"body=" + "-" + "&"; ;
        return View("Home_Index");
    } 
}