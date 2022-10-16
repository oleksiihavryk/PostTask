using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PostTask.Client.Controllers;
using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Tests.TestData.SharedData;
using PostTask.Client.ViewModels;

namespace PostTask.Client.Tests.ControllersTests;
public class ErrorControllerTests
{
    private readonly HttpContext _hc;

    public ErrorControllerTests()
    {
        _hc = new DefaultHttpContext()
        {
            RequestServices = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .Services
                .BuildServiceProvider(),
        };

        _hc.Request.Host = new HostString(string.Empty);
        _hc.Request.Scheme = string.Empty;
        _hc.Features.Set<IExceptionHandlerPathFeature>(new ExceptionHandlerFeature()
        {
            Error = new UnknownException(),
            Path = string.Empty,
            Endpoint = null,
            RouteValues = null
        });
    }
    [Fact]
    public async Task ExceptionTypeHandler_Index_HandleError_ShouldWorkCorrect()
    {
        //arrange
        var eh = new ExceptionTypeErrorHandler();
        var c = new ErrorController(eh);
        c.ControllerContext.HttpContext = _hc;

        //act
        var res = await c.Index();

        //assert
        var viewModel = Assert.IsType<ErrorViewModel>(res.Model);
        Assert.NotNull(viewModel);
        Assert.Equal(
            expected: "Error_Index",
            actual: res.ViewName);
    }
}