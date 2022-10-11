﻿using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using PostTask.Authentication.Core.Exceptions;
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
    public IDictionary<string, Func<User, string>> ClaimsAndActions { get; } =
        new Dictionary<string, Func<User, string>>
        {
            [ClaimTypes.Name] = u => u.UserName,
            [ClaimTypes.NameIdentifier] = u => u.Id,
            [JwtClaimTypes.Subject] = u => u.Id
        };
    public string RoleClaimType { get; } = ClaimTypes.Role;

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
        if ((await _userManager.FindByNameAsync(user.UserName)) == null)
            throw new UserNotFoundException(user.UserName, user.Id);

        var claims = new List<Claim>();
        foreach (var claimAndAction in ClaimsAndActions)
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
        if ((await _userManager.FindByNameAsync(user.UserName)) == null)
            throw new UserNotFoundException(user.UserName, user.Id);

        var claim = new Claim(RoleClaimType, role.ToString());
        await _userManager.AddClaimAsync(user, claim);
    }
}