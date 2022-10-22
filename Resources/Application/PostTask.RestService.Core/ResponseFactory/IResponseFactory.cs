using System.Net;
using PostTask.RestService.Shared;

namespace PostTask.RestService.Core.ResponseFactory;
/// <summary>
///     Response factory service. Help faster create service responses.
/// </summary>
public interface IResponseFactory
{
    /// <summary>
    ///     Empty success response
    /// </summary>
    Response EmptySuccess { get; }
    /// <summary>
    ///     Internal server error response
    /// </summary>
    Response InternalServerErrorResponse { get; }

    /// <summary>
    ///     Create success server response
    /// </summary>
    /// <param name="responseType">
    ///     Response type of response
    /// </param>
    /// <param name="message">
    ///     Response message
    /// </param>
    /// <param name="object">
    ///     Response object
    /// </param>
    /// <returns>
    ///     Response object model 
    /// </returns>
    Response CreateSuccessResponse(
        SuccessResponseType responseType,
        string? message = null,
        object? @object = null);
    /// <summary>
    ///     Create failed server response
    /// </summary>
    /// <param name="responseType">
    ///     Response type of response
    /// </param>
    /// <param name="message">
    ///     Response message
    /// </param>
    /// <param name="object">
    ///     Response object
    /// </param>
    /// <returns>
    ///     Response object model 
    /// </returns>
    Response CreateFailedResponse(
        FailedResponseType responseType,
        string? message = null,
        object? @object = null);
}