using PostTask.Client.Controllers;

namespace PostTask.Client.Tests.ControllerTests.Home;
public class ActionsTests
{
    [Fact]
    public void HomeController_IndexActionTest()
    {
        //arrange
        var h = new HomeController();

        //act
        var res = h.Index();
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
}