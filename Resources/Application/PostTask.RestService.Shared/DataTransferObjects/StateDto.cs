using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;

/// <summary>
///     Data transfer object of task state
/// </summary>
public class StateDto
{
    /// <summary>
    ///     State identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     State name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;
}