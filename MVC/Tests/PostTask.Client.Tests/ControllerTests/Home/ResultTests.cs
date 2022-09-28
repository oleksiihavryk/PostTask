using PostTask.Client.Controllers;

namespace PostTask.Client.Tests.ControllerTests.Home;
public class ResultTests
{
    [Fact]
    public void HomeController_ResultIsHomeIndexView_ReturnsTrue()
    {
        //arrange
        var h = new HomeController();

        //act
        var res = h.Index();

        //assert
        Assert.Equal(
            expected: "Home_Index",
            actual: res.ViewName);
    }
}