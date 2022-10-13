using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;

/// <summary>
///     State of task executing
/// </summary>
public class State : IIdentifiable
{
    /// <summary>
    ///     State identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     State name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}