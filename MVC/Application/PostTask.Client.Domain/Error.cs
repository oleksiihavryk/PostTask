namespace PostTask.Client.Domain;
/// <summary>
///     Error model for user view
/// </summary>
public sealed class Error
{
    /// <summary>
    ///     Error name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Error message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}