using System.Collections;
using System.Runtime.Serialization;
using PostTask.RestService.Shared.Extensions;

namespace PostTask.RestService.Data.Exceptions;
/// <summary>
///     Object not found exception model
/// </summary>
[Serializable]
public class ObjectNotFoundException : Exception
{
    /// <summary>
    ///     Data key of source field
    /// </summary>
    public const string SourceDataKey = "Source";

    /// <summary>
    ///     Source of searching object
    /// </summary>
    private readonly object? _source = null;

    public override string Message => base.Message ??
                                      "Object not found in " +
                                      $"{(_source == null ? "unknown" : "defined")} source. " +
                                      "Check stack trace and exception data to get more " +
                                      "information about exception";
    public override IDictionary Data => 
        new Dictionary<string, object?>()
        {
            [SourceDataKey] = _source
        };

    /// <summary>
    ///     Create object not found exception model with defined source and message
    /// </summary>
    /// <param name="source">
    ///     Source of finding object when exception is occurred
    /// </param>
    /// <param name="message">
    ///     Exception model message
    /// </param>
    public ObjectNotFoundException(
        object? source, 
        string? message = null)
        : this(source, message, null)
    {
    }
    /// <summary>
    ///     Create object not found exception model with defined source,
    ///     message and inner exception
    /// </summary>
    /// <param name="source">
    ///     Source of finding object when exception is occurred
    /// </param>
    /// <param name="message">
    ///     Exception model message
    /// </param>
    /// <param name="inner">
    ///     Exception model inner exception
    /// </param>
    public ObjectNotFoundException(
        object? source, 
        string? message, 
        Exception? inner = null)
        : base(message, inner)
    {
        _source = source;
    }
    private ObjectNotFoundException(
        SerializationInfo info,
        StreamingContext context)
    {
        try
        {
            _source = info.GetValue<object?>(nameof(_source));
        }
        catch (KeyNotFoundException)
        {
            //ignore..
        }
    }
}