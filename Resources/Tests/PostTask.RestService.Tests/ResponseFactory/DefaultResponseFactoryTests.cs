using PostTask.RestService.Core.ResponseFactory;

namespace PostTask.RestService.Tests.ResponseFactory;
public class DefaultResponseFactoryTests
{
    private readonly DefaultResponseFactory _responseFactory = 
        new DefaultResponseFactory();

    [Fact]
    public void CreateSuccessResponse_WithoutMessage_GetMessageByDefaultFromHandler()
    {
        //arrange
        var responseType = SuccessResponseType.EmptySuccess;
        //act
        var response = _responseFactory.CreateSuccessResponse(
            responseType,
            @object: new object());
        //assert
        Assert.NotNull(response);
        var successHandler = 
            _responseFactory.SuccessResponseDataHandler;
        successHandler.TryGetValue(responseType, out var responseData);
        Assert.NotNull(responseData);
        Assert.Equal(
            expected: responseData?.Message,
            actual: response.Result.Message);
    }
    [Fact]
    public void CreateFailedResponse_WithoutMessage_GetMessageByDefaultFromHandler()
    {
        //arrange
        var responseType = FailedResponseType.NotFound;
        //act
        var response = _responseFactory.CreateFailedResponse(
            responseType,
            @object: new object());
        //assert
        Assert.NotNull(response);
        var failedHandler =
            _responseFactory.FailedResponseDataHandler;
        failedHandler.TryGetValue(responseType, out var responseData);
        Assert.NotNull(responseData);
        Assert.Equal(
            expected: responseData?.Message,
            actual: response.Result.Message);
    }
    [Fact]
    public void CreateSuccessResponse_WithPassedSuccessType_ReturnCorrectHandledStatusCode()
    {
        //arrange
        var successType = SuccessResponseType.Success;
        var @object = new object();
        var message = string.Empty;
        //act
        var response = _responseFactory.CreateSuccessResponse(
            responseType: successType,
            message,
            @object);
        //assert
        Assert.NotNull(response);
        var successHandler = 
            _responseFactory.SuccessResponseDataHandler;
        Assert.Equal(
            expected: successHandler[successType].StatusCode,
            actual: response.StatusCode);
        Assert.Equal(
            expected: @object,
            actual: response.Result.Object);
        Assert.Equal(
            expected: message,
            actual: response.Result.Message);
    }
    [Fact]
    public void CreateFailedResponse_WithPassedFailedType_ReturnCorrectHandledStatusCode()
    {
        //arrange
        var successType = FailedResponseType.ServerError;
        var @object = new object();
        var message = string.Empty;
        //act
        var response = _responseFactory.CreateFailedResponse(
            responseType: successType,
            message,
            @object);
        //assert
        Assert.NotNull(response);
        var failedHandler =
            _responseFactory.FailedResponseDataHandler;
        Assert.Equal(
            expected: failedHandler[successType].StatusCode,
            actual: response.StatusCode);
        Assert.Equal(
            expected: @object,
            actual: response.Result.Object);
        Assert.Equal(
            expected: message,
            actual: response.Result.Message);
    }
}