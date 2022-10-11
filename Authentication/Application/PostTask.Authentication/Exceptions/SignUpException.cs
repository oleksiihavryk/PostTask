namespace PostTask.Authentication.Exceptions;
/// <summary>
///     Sign up error exception model
/// </summary>
public class SignUpException : Exception
{
    public SignUpException(string? message = null)
        : base(message)
    {
    }
    public SignUpException(
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
    }
}