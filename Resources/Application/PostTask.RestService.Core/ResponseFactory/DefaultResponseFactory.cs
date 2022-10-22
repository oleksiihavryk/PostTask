using System.Net;
using PostTask.RestService.Shared;

namespace PostTask.RestService.Core.ResponseFactory;

/// <summary>
///     Default response factory implementation. Create default response
/// </summary>
public class DefaultResponseFactory : IResponseFactory
{
    /// <summary>
    ///     Success response data handler, help to handle response types by creating response
    /// </summary>
    public virtual Dictionary<SuccessResponseType, ResponseData> SuccessResponseDataHandler
        => new()
        {
            [SuccessResponseType.Created] = new ResponseData(
                message: "Object was successfully created.",
                statusCode: (int)HttpStatusCode.Created),
            [SuccessResponseType.Success] = new ResponseData(
                message: "Operation was successful.",
                statusCode: (int)HttpStatusCode.OK),
            [SuccessResponseType.EmptySuccess] = new ResponseData(
                message: "Object is received successfully, but his empty.",
                statusCode: (int)HttpStatusCode.NoContent)
        };
    /// <summary>
    ///     Failed response data handler, help to handle response types by creating response
    /// </summary>
    public virtual Dictionary<FailedResponseType, ResponseData> FailedResponseDataHandler
        => new()
        {
            [FailedResponseType.IncorrectInput] = new ResponseData(
                message: "Received data input was incorrect.",
                statusCode: (int)HttpStatusCode.BadRequest),
            [FailedResponseType.NotFound] = new ResponseData(
                message: "Requested data is not found.",
                statusCode: (int)HttpStatusCode.NotFound),
            [FailedResponseType.ServerError] = new ResponseData(
                message: "On server side occurred unknown error.",
                statusCode: (int)HttpStatusCode.InternalServerError)

        };

    public Response EmptySuccess => CreateSuccessResponse(
        responseType: SuccessResponseType.Success,
        message: string.Empty);
    public Response InternalServerErrorResponse => CreateFailedResponse(
        responseType: FailedResponseType.ServerError,
    message: string.Empty);

    public Response CreateSuccessResponse(
        SuccessResponseType responseType, 
        string? message = null, 
        object? @object = null)
    {
        if (!SuccessResponseDataHandler.TryGetValue(key: responseType, value: out var responseData))
            throw new ArgumentException(
                message: "Response type is impossible to handle by this response factory");

        return CreateResponse(
            statusCode: responseData.StatusCode,
            message: message ?? responseData.Message,
            @object: @object,
            isSuccess: true);
    }
    public Response CreateFailedResponse(
        FailedResponseType responseType, 
        string? message = null, 
        object? @object = null)
    {
        if (!FailedResponseDataHandler.TryGetValue(key: responseType, value: out var responseData))
            throw new ArgumentException(
                message: "Response type is impossible to handle by this response factory");

        return CreateResponse(
            statusCode: responseData.StatusCode,
            message: message ?? responseData.Message,
            @object: @object,
            isSuccess: false);
    }

    /// <summary>
    ///     Create response object model
    /// </summary>
    /// <param name="statusCode">
    ///     Status code of response
    /// </param>
    /// <param name="message">
    ///     Response message
    /// </param>
    /// <param name="object">
    ///     Response object
    /// </param>
    /// <param name="isSuccess">
    ///     Set true if response is success, otherwise false
    /// </param>
    /// <returns>
    ///     Configured response object
    /// </returns>
    private Response CreateResponse(
        int statusCode,
        string message,
        object? @object,
        bool isSuccess)
    {
        var responseResult = new ResponseResult()
        {
            Message = message,
            Object = @object
        };

        return new Response(responseResult)
        {
            IsSuccess = isSuccess,
            StatusCode = statusCode
        };
    }
}