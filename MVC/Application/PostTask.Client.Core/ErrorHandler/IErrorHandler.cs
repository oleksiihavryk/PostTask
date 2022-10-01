using PostTask.Client.Domain;

namespace PostTask.Client.Core.ErrorHandler;
/// <summary>
///     Error handler service
/// </summary>
public interface IErrorHandler
{
    /// <summary>
    ///     Handle exception model and return error model
    /// </summary>
    /// <param name="ex">
    ///     Exception model
    /// </param>
    /// <returns>
    ///     Returns error model
    /// </returns>
    Error Handle(Exception ex);
}