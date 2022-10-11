using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Domain;
using PostTask.Authentication.Exceptions;
using PostTask.Authentication.Extensions;
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
    private readonly UserManager<User> _userManager;

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
        _userManager = userManager;
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
    /// <summary>
    ///     User login in system from form input
    /// </summary>
    /// <param name="viewModel">
    ///     User login form input view model
    /// </param>
    /// <returns>
    ///     Result of login user in system
    /// </returns>
    [HttpPost("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(viewModel.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user,
                    viewModel.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                    return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(
                key: string.Empty,
                errorMessage: "Unknown user name or password");
        }

        return View(viewModel);
    }
    /// <summary>
    ///     User register in system from form input
    /// </summary>
    /// <param name="viewModel">
    ///     User register form input view model
    /// </param>
    /// <returns>
    ///     Result of register user in system
    /// </returns>
    [HttpPost("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var pass = viewModel.Password;
            var user = new User()
            {
                UserName = viewModel.Username
            };
            var role = Roles.User;

            try
            {
                var identityResult = await _userManager.CreateAsync(
                    user,
                    password: pass);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role.ToString());
                    await _claimProvider.ProvideAsync(user);
                    await _claimProvider.ProvideRoleClaimAsync(user, role);

                    var signInResult = await _signInManager.PasswordSignInAsync(
                        user,
                        password: pass,
                        isPersistent: false,
                        lockoutOnFailure: false);

                    if (!signInResult.Succeeded)
                        throw new SignUpException();

                    return Redirect(viewModel.ReturnUrl);
                }

                ModelState.AddIdentityErrors(identityResult);
            }
            catch (SignUpException)
            {
                await _userManager.DeleteAsync(user);
                ModelState.AddModelError(
                    key: string.Empty,
                    errorMessage: "Unknown error by creating user. Try again later!");
            }
        }

        return View(viewModel);
    }
}