using PostTask.Client.Domain;

namespace PostTask.Client.Core.ErrorHandler;
/// <summary>
///     Error handler implementation handling by exception type
/// </summary>
public sealed class ExceptionTypeErrorHandler : IErrorHandler
{
    /// <summary>
    ///     Type and error model dictionary for exception handler mechanism
    /// </summary>
    public IDictionary<Type, Error> TypeErrorModel { get; } =
        new Dictionary<Type, Error>()
        {
            [typeof(NotImplementedException)] = new Error()
            {
                Name = "Not implemented functionality",
                Message = "This part of functionality currently not working. " +
                          "Try again later."
            }
        };
    /// <summary>
    ///     Default error what occurred when exception is unhandled by handler
    /// </summary>
    public Error DefaultError { get; } = new Error()
    {
        Name = "Server side error",
        Message = "Unknown error on server side."
    };

    /// <summary>
    ///     Handle exception by type and get error model
    /// </summary>
    /// <param name="ex">
    ///     Exception model
    /// </param>
    /// <returns>
    ///     Error model
    /// </returns>
    public Error Handle(Exception ex)
    {
        Error? result = null;

        try
        {
            result = TypeErrorModel[ex.GetType()];
        }
        catch (KeyNotFoundException)
        {
            result = DefaultError;
        }

        return result;
    }
}