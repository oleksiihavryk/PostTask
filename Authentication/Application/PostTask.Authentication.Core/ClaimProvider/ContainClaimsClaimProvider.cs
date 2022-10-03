using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Core.ClaimProvider;
/// <summary>
///     Claim provider for user with type "User" by containment claims
/// </summary>
public class ContainClaimsClaimProvider : IClaimProvider<User>
{
    /// <summary>
    ///     User manager for providing claims
    /// </summary>
    private readonly UserManager<User> _userManager;
    /// <summary>
    ///     Dictionary of claims and action which take a user and return value of claim
    /// </summary>
    private readonly IDictionary<string, Func<User, string>> _claimsAndActions =
        new Dictionary<string, Func<User, string>>
        {
            [JwtClaimTypes.PreferredUserName] = u => u.UserName,
            [JwtClaimTypes.Id] = u => u.Id
        };

    public ContainClaimsClaimProvider(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    ///     Implement of user claims provider service.
    ///     Provide claims to chosen user
    /// </summary>
    /// <param name="user">
    ///     Chosen user
    /// </param>
    /// <returns>
    ///     Returns task of async operation by providing claims to user
    /// </returns>
    public async Task ProvideAsync(User user)
    {
        var claims = new List<Claim>();
        foreach (var claimAndAction in _claimsAndActions)
            claims.Add(new Claim(claimAndAction.Key, claimAndAction.Value(user)));

        await _userManager.AddClaimsAsync(user, claims);
    }
    /// <summary>
    ///     Implement of user claims provider service.
    ///     Provide role claim for user
    /// </summary>
    /// <param name="user">
    ///     User for whom role claim is providing
    /// </param>
    /// <param name="role">
    ///     User role
    /// </param>
    /// <returns>
    ///     Returns task of async operation by providing role claim to user
    /// </returns>
    public async Task ProvideRoleClaimAsync(User user, Roles role)
    {
        var claim = new Claim(JwtClaimTypes.Role, role.ToString());
        await _userManager.AddClaimAsync(user, claim);
    }
}