using System.Collections;
using System.Runtime.Serialization;
using PostTask.Authentication.Shared.Extensions;

namespace PostTask.Authentication.Core.Exceptions;
/// <summary>
///     User not found exception model
/// </summary>
[Serializable]
public class UserNotFoundException : Exception
{
    /// <summary>
    ///     Serialization key for user name field
    /// </summary>
    private const string UserNameSerializationKey = "UserName";
    /// <summary>
    ///     Serialization key for user identifier field
    /// </summary>
    private const string UserIdentifierSerializationKey = "UserId";

    /// <summary>
    ///     Username exception data
    /// </summary>
    private readonly string? _userName;
    /// <summary>
    ///     User identifier exception data
    /// </summary>
    private readonly string? _userId;

    public override string Message => base.Message ??
                                      "User not found in unknown source.";

    public override IDictionary Data =>
        new Dictionary<string, object?>()
        {
            [nameof(_userName)] = _userName,
            [nameof(_userId)] = _userId
        };

    public UserNotFoundException(
        string? userName, 
        string? userId,
        string? message = null)
        : base(message)
    {
        _userName = userName;
        _userId = userId;
    }
    public UserNotFoundException(
        string? userName, 
        string? userId, 
        string? message, 
        Exception? inner = null)
        : base(message, inner)
    {
        _userName = userName;
        _userId = userId;
    }
    private UserNotFoundException(
        SerializationInfo info, 
        StreamingContext context)
    {
        _userId = info.GetValue<string>(UserIdentifierSerializationKey);
        _userName = info.GetValue<string>(UserNameSerializationKey);
    }
}