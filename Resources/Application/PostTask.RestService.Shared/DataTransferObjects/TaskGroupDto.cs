using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;

/// <summary>
///     Data transfer object of task group
/// </summary>
public class TaskGroupDto
{
    /// <summary>
    ///     Group identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     Group name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Group description
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    [MaxLength(256)]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    ///     Group user identifier 
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    ///     All containing tasks in group
    /// </summary>
    public virtual IEnumerable<TaskDto> Items { get; set; } =
        Enumerable.Empty<TaskDto>();
}