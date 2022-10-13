namespace PostTask.RestService.Domain.Exceptions;
/// <summary>
///     Incorrect time input exception model
/// </summary>
[Serializable]
public class IncorrectTimeInputException : Exception
{
    public override string Message => base.Message ?? "Incorrect time input.";

    public IncorrectTimeInputException(string? message = null)  
        : base(message)
    {
    }
    public IncorrectTimeInputException(
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
    }
}