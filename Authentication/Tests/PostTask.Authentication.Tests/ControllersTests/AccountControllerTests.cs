using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostTask.Authentication.Controllers;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Domain;
using PostTask.Authentication.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PostTask.Authentication.Tests.ControllersTests;
public class AccountControllerTests
{
    private readonly string _postLogoutRedirectUri = "post";
    private readonly Dictionary<User, string> _userPasswords = new Dictionary<User, string>();
    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        //new SignInManager<User>()
        var storeMock = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(
            args: new object?[]
            {
                storeMock.Object, null, null, null, null, null, null, null, null
            });
        SetupUserManagerMock(userManagerMock);

        var accessorMock = new Mock<IHttpContextAccessor>();
        var claimMock = new Mock<IUserClaimsPrincipalFactory<User>>();
        var signInMock = new Mock<SignInManager<User>>(
            args: new object?[]
            {
                userManagerMock.Object, accessorMock.Object, claimMock.Object, 
                null, null, null, null
            });
        SetupSignInMock(signInMock);

        var claimProviderMock = new Mock<IClaimProvider<User>>();
        SetupClaimProviderMock(claimProviderMock);
        
        var interactionServiceMock = new Mock<IIdentityServerInteractionService>();
        SetupInteractionServiceMock(interactionServiceMock);

        _accountController = new AccountController(
            signInManager: signInMock.Object,
            userManager: userManagerMock.Object,
            claimProvider: claimProviderMock.Object,
            interactionService: interactionServiceMock.Object);
    }

    [Fact]
    public async Task LoginPage_Invoke_ShouldWorkCorrect()
    {
        //arrange
        string returnUrl = "llll";
        //none
        //act
        var resultPage = await _accountController.Login(returnUrl);
        //assert
        Assert.Null(resultPage.ViewName);
        var model = Assert.IsType<LoginViewModel>(resultPage.Model);
        Assert.Equal(
            expected: returnUrl,
            actual: model.ReturnUrl);
    }
    [Fact]
    public async Task RegisterPage_Invoke_ShouldWorkCorrect()
    {
        //arrange
        string returnUrl = "llll";
        //act
        var resultPage = await _accountController.Register(returnUrl);
        //assert
        Assert.Null(resultPage.ViewName);
        var model = Assert.IsType<RegisterViewModel>(resultPage.Model);
        Assert.Equal(
            expected: returnUrl,
            actual: model.ReturnUrl);
    }
    [Fact]
    public async Task Logout_Invoke_ShouldWorkCorrect()
    {
        //arrange
        //none
        //act
        var resultPage = await _accountController.Logout(string.Empty);
            // logoutId is not important value because his provided by identity server
        //assert
        Assert.Equal(expected: _postLogoutRedirectUri,
            actual: resultPage.Url);
    }
    [Fact]
    public async Task LoginPost_ReceivingIncorrectModelState_ShouldReturnLoginPageWithErrors()
    {
        //arrange
        var viewModel = new LoginViewModel()
        {
            Password = "sapsa",
            ReturnUrl = string.Empty
        };
        _accountController.ModelState.AddModelError(
            key: nameof(viewModel.ReturnUrl),
            errorMessage: "some error");
        //act
        var result = await _accountController.Login(viewModel);

        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
        Assert.Equal(
            expected: viewModel,
            actual: viewResult.Model);
        Assert.NotEmpty(collection: viewResult.ViewData
            .ModelState
            .Where(c => c.Value?.Errors.Any() == true));
    }
    [Fact]
    public async Task LoginPost_ReceivingUnknownUsername_ShouldReturnLoginPageWithLoginError()
    {
        //arrange
        var viewModel = new LoginViewModel()
        {
            Username = "swolota",
            Password = "Pass",
            ReturnUrl = "Rertu"
        };

        //act
        var result = await _accountController.Login(viewModel);

        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
        Assert.Equal(
            expected: viewModel,
            actual: viewResult.Model);
        Assert.Contains(
            expected: AccountController.UsernameOrPasswordIsIncorrectErrorMessage,
            collection: viewResult
                .ViewData
                .ModelState
                .Select(c => c.Value?
                    .Errors
                    .FirstOrDefault()?
                    .ErrorMessage));
    }
    [Fact]
    public async Task LoginPost_ReceivingIncorrectPassword_ShouldReturnLoginPageWithLoginError()
    {
        //arrange
        var user = new User()
        {
            UserName = "swolota"
        };
        string pass = "pass";
        _userPasswords.Add(user, pass);
        var viewModel = new LoginViewModel()
        {
            Username = user.UserName,
            Password = pass + "s",
            ReturnUrl = "|||"
        };

        //act
        var result = await _accountController.Login(viewModel);

        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
        Assert.Equal(
            expected: viewModel,
            actual: viewResult.Model);
        Assert.Contains(
            expected: AccountController.UsernameOrPasswordIsIncorrectErrorMessage,
            collection: viewResult
                .ViewData
                .ModelState
                .Select(c => c.Value?
                    .Errors
                    .FirstOrDefault()?
                    .ErrorMessage));
    }
    [Fact]
    public async Task LoginPost_ReceivingCorrectUserData_ShouldReturnRedirectResult()
    {
            //arrange
            var user = new User()
            {
                UserName = "swolota"
            };
            string pass = "pass";
            _userPasswords.Add(user, pass);
            var viewModel = new LoginViewModel()
            {
                Username = user.UserName,
                Password = pass,
                ReturnUrl = "|||"
            };

            //act
            var result = await _accountController.Login(viewModel);

            //assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.NotNull(redirectResult);
            Assert.Equal(
                expected: viewModel.ReturnUrl,
                actual: redirectResult.Url);
    }
    [Fact]
    public async Task RegisterPost_ReceivingIncorrectModelState_ShouldReturnRegisterPageWithErrors()
    {
        //arrange
        var viewModel = new RegisterViewModel()
        {
            Password = "psa",
            RepeatPassword = "pass",
            ReturnUrl = string.Empty,
        };
        _accountController.ModelState.AddModelError(
            key: nameof(viewModel.ReturnUrl),
            errorMessage: "some error");
        //act
        var result = await _accountController.Register(viewModel);
        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
        Assert.NotEmpty(collection: viewResult.ViewData
            .ModelState
            .Where(c => c.Value?
                .Errors
                .Any() == true));
        Assert.Null(viewResult.ViewName);
    }
    [Fact]
    public async Task RegisterPost_ReceivingCorrectInput_ShouldWorkCorrect()
    {
        //arrange
        var viewModel = new RegisterViewModel()
        {
            Password = "Password",
            RepeatPassword = "Password",
            ReturnUrl = "|||",
            Username = "XoXiIl123"
        };
        int userCountBefore = _userPasswords.Count;
        //act
        var result = await _accountController.Register(viewModel);
        //assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.NotNull(redirectResult);
        Assert.Equal(
            expected: viewModel.ReturnUrl,
            actual: redirectResult.Url);
        Assert.Equal(
            expected: userCountBefore + 1,
            actual: _userPasswords.Count);
        bool inCollection = false;
        foreach (var up in _userPasswords)
        {
            var u = up.Key;
            if (u.UserName == viewModel.Username)
            {
                inCollection = true;
                break;
            }
        }
        Assert.True(inCollection);
    }

    private void SetupInteractionServiceMock(
        Mock<IIdentityServerInteractionService> interactionServiceMock)
    {
        interactionServiceMock
            .Setup(expression:
                iis => iis.GetLogoutContextAsync(It.IsAny<string>()))
            .Returns<string>(
                id => Task.FromResult(
                    new LogoutRequest(
                        iframeUrl: string.Empty,
                        message: new Mock<LogoutMessage>().Object)
                    {
                        PostLogoutRedirectUri = _postLogoutRedirectUri
                    }));
    }
    private void SetupClaimProviderMock(Mock<IClaimProvider<User>> claimProviderMock)
    {
        claimProviderMock
            .Setup(expression:
                cp => cp.ProvideAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        claimProviderMock
            .Setup(expression:
                cp => cp.ProvideRoleClaimAsync(
                    It.IsAny<User>(),
                    It.IsAny<Roles>()))
            .Returns(Task.CompletedTask);
    }
    private void SetupUserManagerMock(Mock<UserManager<User>> userManagerMock)
    {
        userManagerMock
            .Setup(expression:
                um => um.FindByNameAsync(It.IsAny<string>()))
            .Returns<string>(name =>
            {
                var user = _userPasswords.Keys.FirstOrDefault(u => u.UserName == name);
                return Task.FromResult(user);
            });
        userManagerMock
            .Setup(expression:
                um => um.CreateAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>()))
            .Returns<User, string>((user, pass) =>
            {
                _userPasswords[user] = pass;
                return Task.FromResult(IdentityResult.Success);
            });
    }
    private void SetupSignInMock(Mock<SignInManager<User>> signInMock)
    {
        signInMock
            .Setup(expression: 
                si => si.SignOutAsync())
            .Returns(Task.CompletedTask);
        signInMock
            .Setup(expression:
                si => si.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
            .Returns<User, string, bool, bool>(
                (user, pass, pers, @lock) =>
                     Task.FromResult(
                        _userPasswords.TryGetValue(user, out var userPass) && 
                            userPass == pass ? 
                            SignInResult.Success : SignInResult.Failed));
    }
}