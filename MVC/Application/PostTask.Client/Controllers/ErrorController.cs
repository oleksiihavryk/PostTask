using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Extensions;
using PostTask.Client.ViewModels;

namespace PostTask.Client.Controllers;
/// <summary>
///     Error handler controller
/// </summary>
public sealed class ErrorController : Controller
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
    public ViewResult Index()
    {
        var errorHandlerFeature = HttpContext
            .RequestServices
            .GetRequiredService<IExceptionHandlerFeature>();
        var exception = errorHandlerFeature.Error;
        
        var error = _eh.Handle(ex: exception);
        var fullPath = errorHandlerFeature.GetFullPath();

        var viewModel = new ErrorViewModel()
        {
            FullPath = fullPath,
            Message = error.Message,
            Name = error.Name
        };

        return View(viewModel);
    }
}