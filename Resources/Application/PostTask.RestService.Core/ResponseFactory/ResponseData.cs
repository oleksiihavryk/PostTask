namespace PostTask.RestService.Core.ResponseFactory;
/// <summary>
///     Response data object
/// </summary>
public sealed class ResponseData
{
    /// <summary>
    ///     Response message
    /// </summary>
    public string Message { get; }
    /// <summary>
    ///     Response status code
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    ///     Create default response data object
    /// </summary>
    /// <param name="message">
    ///     Response message
    /// </param>
    /// <param name="statusCode">
    ///     Response status code
    /// </param>
    public ResponseData(
        string message,
        int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}