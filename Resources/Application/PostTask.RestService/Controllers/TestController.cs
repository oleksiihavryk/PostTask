using Microsoft.AspNetCore.Mvc;
using PostTask.RestService.Core.ResponseFactory;

namespace PostTask.RestService.Controllers;
[ApiController]
[Route("test")]
public sealed class TestController : ControllerBase
{
    private readonly IResponseFactory _responseFactory;

    public TestController(IResponseFactory responseFactory)
    {
        _responseFactory = responseFactory;
    }

    [HttpGet("result")]
    public async Task<OkObjectResult> GetResult()
        => await Task.Run(() =>
        {
            var data = new[]
            {
                "Parash",
                "Pamash",
                "Daram"
            };

            var response = _responseFactory.CreateSuccessResponse(
                responseType: SuccessResponseType.Success,
                @object: data);

            return Ok(response);
        });
    [HttpGet("not-found")]
    public new async Task<OkObjectResult> NotFound()
        => await Task.Run(() =>
        {
            var data = new[]
            {
                "Parash",
                "Pamash",
                "Daram"
            };

            var response = _responseFactory.CreateFailedResponse(
                responseType: FailedResponseType.NotFound,
                @object: data);

            return Ok(response);
        });
    [HttpGet("exception")]
    public Task<OkObjectResult> Exception()
        => throw new NotImplementedException();
}