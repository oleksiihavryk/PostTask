using Newtonsoft.Json;

namespace PostTask.RestService.Shared;
/// <summary>
///     Response result model
/// </summary>
public sealed class ResponseResult
{
    /// <summary>
    ///     Response result object
    /// </summary>
    public object? Object { get; set; } = null;
    /// <summary>
    ///     Response result message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    ///     Return json string that represent a current object
    /// </summary>
    /// <returns>
    ///     JSON representation of object
    /// </returns>
    public override string ToString()
        => JsonConvert.SerializeObject(this);
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is ResponseResult other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Object, Message);

    /// <summary>
    ///     Checks on equality response results among themselves
    /// </summary>
    /// <param name="other">
    ///     Other response result object
    /// </param>
    /// <returns>
    ///     Result of equality between objects
    /// </returns>
    private bool Equals(ResponseResult other)
        => Equals(Object, other.Object) &&
           Message == other.Message;
}