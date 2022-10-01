using Microsoft.AspNetCore.Diagnostics;

namespace PostTask.Client.Extensions;
/// <summary>
///     Extensions to exception handler
/// </summary>
public static class ErrorHandlerExtensions
{
    /// <summary>
    ///     Get full path of error occurred place
    /// </summary>
    /// <param name="eh">
    ///     Exception handler service feature
    /// </param>
    /// <returns>
    ///     Path string
    /// </returns>
    public static string GetFullPath(this IExceptionHandlerFeature eh)
        => eh.Path;
}