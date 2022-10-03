using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Extensions;
using PostTask.Client.ViewModels;

namespace PostTask.Client.Controllers;
/// <summary>
///     Error handler controller
/// </summary>
// Note: that is a non sealed class because his used in unit testing
public class ErrorController : Controller
{
    /// <summary>
    ///     Error handler service
    /// </summary>
    private readonly IErrorHandler _eh;

    public ErrorController(IErrorHandler eh)
    {
        _eh = eh;
    }

    /// <summary>
    ///     Error handler view
    /// </summary>
    /// <returns>
    ///     View with handled error
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred if exception handler feature is unavailable (impossible situation)
    /// </exception>
    [Route("/Error")]
    public async Task<ViewResult> Index()
        => await Task.Run(() =>
        {
            var errorHandlerFeature = HttpContext
                                          .Features
                                          .Get<IExceptionHandlerPathFeature>() ??
                                      throw new InvalidOperationException("Impossible error");
            var exception = errorHandlerFeature.Error;

            var error = _eh.Handle(ex: exception);
            var schemeHostUrl = Request.Scheme + "://" + Request.Host;
            var fullPath = errorHandlerFeature.GetFullPath(schemeHostUrl);

            var viewModel = new ErrorViewModel()
            {
                FullPath = fullPath,
                Message = error.Message,
                Name = error.Name
            };

            return View("Error_Index", viewModel);
        });
    /// <summary>
    ///     Invoke error on purpose (only for unfinished application)
    /// </summary>
    /// <exception cref="NotImplementedException">
    ///     Occurred always
    /// </exception>
    public void Unimplemented() => throw new NotImplementedException();
}