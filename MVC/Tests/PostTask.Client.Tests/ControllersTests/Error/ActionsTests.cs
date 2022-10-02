using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PostTask.Client.Controllers;
using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Tests.TestData.SharedData;
using PostTask.Client.ViewModels;

namespace PostTask.Client.Tests.ControllersTests.Error;
public class ActionsTests
{
    private readonly HttpContext _hc;

    public ActionsTests()
    {
        _hc = new DefaultHttpContext()
        {
            RequestServices = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .Services
                .BuildServiceProvider(),
        };
        _hc.Features.Set<IExceptionHandlerPathFeature>(new ExceptionHandlerFeature()
        {
            Error = new UnknownException(),
            Path = string.Empty,
            Endpoint = null,
            RouteValues = null
        });
    }

    [Fact]
    public void ErrorController_ActionIndex_OnExceptionTypeErrorHandler()
    {
        //arrange
        var eh = new ExceptionTypeErrorHandler();
        var c = new ErrorController(eh);
        c.ControllerContext.HttpContext = _hc;

        //act
        var res = c.Index();

        //assert
        var viewModel = Assert.IsType<ErrorViewModel>(res.Model);
        Assert.NotNull(viewModel);
        Assert.Equal(
            expected: "Error_Index",
            actual: res.ViewName);
    }
}