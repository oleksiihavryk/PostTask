namespace PostTask.RestService.Core.ResponseFactory;
/// <summary>
///     Failed response types
/// </summary>
public enum FailedResponseType
{
    /// <summary>
    ///     Incorrect input response type
    /// </summary>
    IncorrectInput,
    /// <summary>
    ///     Not found response type
    /// </summary>
    NotFound,
    /// <summary>
    ///     Unknown server error response type
    /// </summary>
    ServerError,
}