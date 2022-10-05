using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Domain;
using PostTask.Authentication.ViewModels;

namespace PostTask.Authentication.Controllers;
/// <summary>
///     Controller for provide single-way authentication on current oidc server
/// </summary>
[Route("[controller]")]
public sealed class AccountController : Controller
{
    /// <summary>
    ///     Sign in manager service
    /// </summary>
    private readonly SignInManager<User> _signInManager;
    /// <summary>
    ///     User manager service
    /// </summary>
    private readonly UserManager<User> _userManagerManager;
    /// <summary>
    ///     Claim provider service
    /// </summary>
    private readonly IClaimProvider<User> _claimProvider;
    /// <summary>
    ///     Server interaction service
    /// </summary>
    private readonly IIdentityServerInteractionService _interactionService;

    public AccountController(
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        IClaimProvider<User> claimProvider, 
        IIdentityServerInteractionService interactionService)
    {
        _signInManager = signInManager;
        _userManagerManager = userManager;
        _claimProvider = claimProvider;
        _interactionService = interactionService;
    }

    /// <summary>
    ///     User login in system action
    /// </summary>
    /// <param name="returnUrl">
    ///     Url from that request is sending to server
    /// </param>
    /// <returns>
    ///     Returns view of login page
    /// </returns>
    [HttpGet("[action]")]
    public ViewResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };
        return View(viewModel);
    }
    /// <summary>
    ///     User register in system action
    /// </summary>
    /// <param name="returnUrl">
    ///     Url from that request is sending to server
    /// </param>
    /// <returns>
    ///     Returns view of register page
    /// </returns>
    [HttpGet("[action]")]
    public async Task<ViewResult> Register(string returnUrl)
        => await Task.Run(() =>
        {
            var viewModel = new RegisterViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        });
    /// <summary>
    ///     User logout from system action
    /// </summary>
    /// <param name="logoutId">
    ///     Logout session identifier
    /// </param>
    /// <returns>
    ///     Returns to session post logout redirect link
    /// </returns>
    [HttpGet("[action]")]
    public async Task<RedirectResult> Logout(string logoutId)
        => await Task.Run(async () =>
        {
            await _signInManager.SignOutAsync();
            var lc = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(lc.PostLogoutRedirectUri);
        });
}