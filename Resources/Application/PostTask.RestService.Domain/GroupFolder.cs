using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;

/// <summary>
///     Couple task groups is contained in folder and united by common meaning
/// </summary>
public sealed class GroupFolder : Folder<TaskGroup>, IIdentifiable
{
}