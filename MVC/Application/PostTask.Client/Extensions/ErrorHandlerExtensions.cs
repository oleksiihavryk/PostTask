using Microsoft.AspNetCore.Diagnostics;

namespace PostTask.Client.Extensions;
/// <summary>
///     Extensions to exception handler
/// </summary>
internal static class ErrorHandlerExtensions
{
    /// <summary>
    ///     Get full path of error occurred place
    /// </summary>
    /// <param name="eh">
    ///     Exception handler service feature
    /// </param>
    /// <param name="schemeHostUrl">
    ///     Part of url with server scheme and host
    /// </param>
    /// <returns>
    ///     Path string
    /// </returns>
    public static string GetFullPath(
        this IExceptionHandlerFeature eh,
        string schemeHostUrl)
        => schemeHostUrl + eh.Path;
}