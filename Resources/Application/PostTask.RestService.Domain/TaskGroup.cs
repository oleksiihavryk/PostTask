using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;

/// <summary>
///     Couple tasks grouped by common meaning and logical separated from other tasks
/// </summary>
public sealed class TaskGroup : Folder<TaskItem>, IIdentifiable
{
}