namespace PostTask.Client.ViewModels;
/// <summary>
///     Error view model
/// </summary>
public sealed class ErrorViewModel
{
    /// <summary>
    ///     Full path of where error occurred
    /// </summary>
    public string FullPath { get; set; } = string.Empty;
    /// <summary>
    ///     Error name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Error message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}