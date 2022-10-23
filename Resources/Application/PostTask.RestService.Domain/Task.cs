using System.ComponentModel.DataAnnotations;
using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;
/// <summary>
///     Task - main application unit, folder of task items
/// </summary>
public sealed class Task : Folder<TaskItem>, IIdentifiable
{
    /// <summary>
    ///     Task state
    /// </summary>
    [Required]
    public State State { get; set; }

    public Task()
        : base()
    {
        State = new State();
    }
    public Task(State state)
        : base()
    {
        State = state;
    }

    /// <summary>
    ///     Modify task item in folder by id and return task item if
    ///     his has been found, otherwise return null
    /// </summary>
    /// <param name="id">
    ///     Task item identifier
    /// </param>
    /// <param name="updateState">
    ///     Action for updating state of task item
    /// </param>
    /// <returns>
    ///     Return null if object has been changed or return null if its not
    /// </returns>
    public TaskItem? ModifyTaskItem(Guid id, Action<TaskItem> updateState)
    {
        var taskItem = this.FirstOrDefault(t => t.Id == id);
        updateState(taskItem ?? new TaskItem());
        return taskItem;
    }
}