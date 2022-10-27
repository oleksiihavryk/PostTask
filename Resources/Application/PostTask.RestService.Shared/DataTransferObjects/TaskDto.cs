using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;
/// <summary>
///     Data transfer object of task
/// </summary>
public class TaskDto
{
    /// <summary>
    ///     Task identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     Task name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Task description
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    [MaxLength(256)]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    ///     Task user identifier 
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    ///     All containing items in folder
    /// </summary>
    public virtual IEnumerable<TaskItemDto> Items { get; set; } =
        Enumerable.Empty<TaskItemDto>();
    /// <summary>
    ///     Task state
    /// </summary>
    public StateDto? State { get; set; } = null;
}