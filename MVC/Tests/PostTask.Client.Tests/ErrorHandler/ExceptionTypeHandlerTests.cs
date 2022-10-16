using PostTask.Client.Core.ErrorHandler;
using PostTask.Client.Tests.TestData.SharedData;

namespace PostTask.Client.Tests.ErrorHandler;
public class ExceptionTypeHandlerTests
{
    private readonly ExceptionTypeErrorHandler _eh;

    public ExceptionTypeHandlerTests()
    {
        _eh = new ExceptionTypeErrorHandler();
    }

    [Fact]
    public void Handle_HandleUnsupportedException_ShouldReturnDefaultError()
    {
        //arrange
        var ue = new UnknownException();

        //act
        var error = _eh.Handle(ue);

        //assert
        Assert.NotNull(error);
        Assert.Equal(error, _eh.DefaultError);
    }
    [Fact]
    public void Handle_HandleNotImplementedException_ShouldHandleCorrect()
    {
        //arrange
        var ue = new NotImplementedException();

        //act
        var error = _eh.Handle(ue);

        //assert
        Assert.NotNull(error);
        Assert.Equal(error, _eh.TypeErrorModel[ue.GetType()]);
    }
}