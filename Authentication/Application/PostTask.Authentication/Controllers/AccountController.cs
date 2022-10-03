using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Controllers;
/// <summary>
///     Controller for provide single-way authentication on current oidc server
/// </summary>
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _user;
    private readonly IClaimProvider<User> _claimProvider;
    private readonly IAuthorizeInteractionResponseGenerator _interactionResponseGenerator;

    public AccountController(
        SignInManager<User> signInManager, 
        UserManager<User> user, 
        IClaimProvider<User> claimProvider, 
        IAuthorizeInteractionResponseGenerator interactionResponseGenerator)
    {
        _signInManager = signInManager;
        _user = user;
        _claimProvider = claimProvider;
        _interactionResponseGenerator = interactionResponseGenerator;
    }

    public Task<ViewResult> Login()
    {
        throw new NotImplementedException();
    }
    public Task<ViewResult> Logout()
    {
        throw new NotImplementedException();
    }
}