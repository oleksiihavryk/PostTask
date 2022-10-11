using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PostTask.Authentication.Extensions;
/// <summary>
///     Model state extensions
/// </summary>
public static class ModelStateExtensions
{
    /// <summary>
    ///     Add identity result errors in model state
    /// </summary>
    /// <param name="modelState">
    ///     Model state provider
    /// </param>
    /// <param name="result">
    ///     Identity result 
    /// </param>
    public static void AddIdentityErrors(
        this ModelStateDictionary modelState,
        IdentityResult result)
    {
        foreach (var e in result.Errors)
            modelState.AddModelError(
                key: string.Empty,
                errorMessage: e.Description);
    }
}