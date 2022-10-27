using System.ComponentModel.DataAnnotations;

namespace PostTask.RestService.Shared.DataTransferObjects;

/// <summary>
///     Data transfer object of group folder
/// </summary>
public class GroupFolderDto
{
    /// <summary>
    ///     Folder identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     Folder name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Folder description
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    [MaxLength(256)]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    ///     Folder user identifier 
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    ///     All containing groups in group folder
    /// </summary>
    public virtual IEnumerable<TaskGroupDto> Items { get; set; } = 
        Enumerable.Empty<TaskGroupDto>();
}