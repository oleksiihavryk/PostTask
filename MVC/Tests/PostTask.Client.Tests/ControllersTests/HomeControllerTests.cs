using PostTask.Client.Controllers;
using PostTask.Client.Shared.Constants;

namespace PostTask.Client.Tests.ControllersTests;
public class HomeControllerTests
{
    private readonly HomeController _homeController = new HomeController(); 

    [Fact]
    public async Task Index_InvokeIndex_ShouldWorkCorrect()
    {
        //arrange
        //none

        //act
        var res = await _homeController.Index();
        var emailLink = res.ViewData[HomeController.EmailKey];

        //assert
        Assert.Equal(
            expected: "Home_Index",
            actual: res.ViewName);
        var assertedString = Assert.IsType<string>(emailLink);
        Assert.Matches(
            expectedRegexPattern: "^https:\\/\\/mail\\.google\\.com\\/mail\\/\\?view=cm&fs=1&to=oleksii\\.havryk2004@gmail\\.com&su=.*&body=.*&$",
            actualString: assertedString);
    }
    [Fact]
    public async Task Login_InvokeLogin_ShouldWorkCorrect()
    {
        //arrange
        //none

        //act
        var redirectResult = await _homeController.Login();
        
        //assert
        Assert.Equal(
            expected: nameof(_homeController.Index),
            actual: redirectResult.ActionName);
    }
    [Fact]
    public async Task Logout_InvokeLogout_ShouldWorkCorrect()
    {
        //arrange
        //none

        //act
        var redirectResult = await _homeController.Logout();

        //assert
        Assert.Equal(
            expected: new List<string>
            {
                AuthenticationConstants.CookieAuthenticationScheme,
                AuthenticationConstants.OidcAuthenticationScheme
            },
            actual: redirectResult.AuthenticationSchemes);
    }
}