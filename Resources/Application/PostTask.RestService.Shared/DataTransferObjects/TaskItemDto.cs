using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;

/// <summary>
///     Data transfer object of task item
/// </summary>
public class TaskItemDto
{
    /// <summary>
    ///     Task step identifier 
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    ///     Task step name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     From what time task step is executing
    /// </summary>
    public DateTime? From { get; set; } = null;
    /// <summary>
    ///     To what time task step is executing
    /// </summary>
    public DateTime? To { get; set; } = null;
    /// <summary>
    ///     Task is done
    /// </summary>
    public bool IsDone { get; set; } = false;
}