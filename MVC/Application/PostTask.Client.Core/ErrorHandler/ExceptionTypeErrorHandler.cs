using PostTask.Client.Domain;

namespace PostTask.Client.Core.ErrorHandler;
/// <summary>
///     Error handler implementation handling by exception type
/// </summary>
public sealed class ExceptionTypeErrorHandler : IErrorHandler
{
    /// <summary>
    ///     Handler default error
    /// </summary>
    private static readonly Error _defaultError = new Error()
    {
        Name = "Server side error",
        Message = "Unknown error on server side."
    };

    /// <summary>
    ///     Type and error model dictionary for exception handler mechanism
    /// </summary>
    private readonly IDictionary<Type, Error> _typeErrorModel =
        new Dictionary<Type, Error>()
        {
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
            result = _typeErrorModel[ex.GetType()];
        }
        catch (KeyNotFoundException)
        {
            result = _defaultError;
        }

        return result;
    }
}