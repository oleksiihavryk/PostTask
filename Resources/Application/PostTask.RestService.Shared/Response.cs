namespace PostTask.RestService.Shared;
/// <summary>
///     Response model
/// </summary>
public sealed class Response
{
    /// <summary>
    ///     Response status code
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    ///     True if response is success, otherwise false
    /// </summary>
    public bool IsSuccess { get; set; }
    /// <summary>
    ///     Response result
    /// </summary>
    public ResponseResult Result { get; set; }

    public Response(ResponseResult responseResult)
    {
        Result = responseResult;
    }
}