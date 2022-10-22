using PostTask.RestService.Core.ResponseFactory;

namespace PostTask.RestService.Middleware;
public sealed class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly IResponseFactory _responseFactory;

    public GlobalExceptionHandlerMiddleware(
        IResponseFactory responseFactory)
    {
        _responseFactory = responseFactory;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch
        {
            HandleException(context);
        }
    }

    private void HandleException(
        HttpContext context)
    {
        var response = _responseFactory.InternalServerErrorResponse;
        response.Result.Message = "Unhandled exception from server side. " +
                                  "Try to connect with main developer to fix this error";
        context.Response.WriteAsJsonAsync(response);
    }
}