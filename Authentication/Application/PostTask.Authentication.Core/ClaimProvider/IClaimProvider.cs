using Microsoft.AspNetCore.Identity;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Core.ClaimProvider;
/// <summary>
///     Claim provider service
/// </summary>
/// <typeparam name="TUser">
///     User type for which claim is providing
/// </typeparam>
public interface IClaimProvider<TUser>
    where TUser : IdentityUser
{
    /// <summary>
    ///     Provides user claims to chosen user
    /// </summary>
    /// <param name="user">
    ///     User for that claims is providing
    /// </param>
    /// <returns>
    ///     Return task of async operation by providing claim to user
    /// </returns>
    Task ProvideAsync(TUser user);
    /// <summary>
    ///     Provide role user claim to chosen user
    /// </summary>
    /// <param name="user">
    ///     User for that claim is providing
    /// </param>
    /// <param name="role">
    ///     Value of provided claim
    /// </param>
    /// <returns>
    ///     Returns task of async operation by providing role claim to user
    /// </returns>
    Task ProvideRoleClaimAsync(TUser user, Roles role);
}