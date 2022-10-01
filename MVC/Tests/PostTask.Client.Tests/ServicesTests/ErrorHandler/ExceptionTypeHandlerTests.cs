using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Tests.TestData.SharedData;

namespace PostTask.Client.Tests.ServicesTests.ErrorHandler;
public class ExceptionTypeHandlerTests
{
    private readonly ExceptionTypeErrorHandler _eh;

    public ExceptionTypeHandlerTests()
    {
        _eh = new ExceptionTypeErrorHandler();
    }

    [Fact]
    public void ErrorHandler_Handle_CustomUnknownException()
    {
        //arrange
        var ue = new UnknownException();

        //act
        var error = _eh.Handle(ue);

        //assert
        Assert.NotNull(error);
        Assert.Equal(error, _eh.DefaultError);
    }
}