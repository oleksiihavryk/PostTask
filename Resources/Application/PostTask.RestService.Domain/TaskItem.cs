using System.ComponentModel.DataAnnotations;
using PostTask.RestService.Domain.Exceptions;
using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;
/// <summary>
///     Step of task executing 
/// </summary>
public sealed class TaskItem : IIdentifiable
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

    /// <summary>
    ///     In one operating changing changing or setting new time to task item
    /// </summary>
    /// <param name="from">
    ///    From time
    /// </param>
    /// <param name="to">
    ///     To time
    /// </param>
    /// <exception cref="IncorrectTimeInputException">
    ///     Occurred if time "from" bigger than time "to"
    /// </exception>
    public void ChangeTime(DateTime? from, DateTime? to)
    {
        if (from != null && to != null && from > to)
            throw new IncorrectTimeInputException(
                message: "Time from cannot be bigger than time to");

        To = to;
        From = from;
    }
}