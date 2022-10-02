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
    [Route("/Error")]
    public ViewResult Index()
    {
        var errorHandlerFeature = HttpContext
            .Features
            .Get<IExceptionHandlerPathFeature>() ?? 
                                  throw new InvalidOperationException("Impossible error");
        var exception = errorHandlerFeature.Error;
        
        var error = _eh.Handle(ex: exception);
        var fullPath = errorHandlerFeature.GetFullPath();

        var viewModel = new ErrorViewModel()
        {
            FullPath = fullPath,
            Message = error.Message,
            Name = error.Name
        };

        return View("Error_Index", viewModel);
    }
    /// <summary>
    ///     Invoke error on purpose (only for unfinished application)
    /// </summary>
    /// <exception cref="NotImplementedException">
    ///     Occurred always
    /// </exception>
    public void Unimplemented() => throw new NotImplementedException();
}