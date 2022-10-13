namespace PostTask.RestService.Domain.Interfaces;
/// <summary>
///     Some object what implement this is identifiable by unique Id
/// </summary>
public interface IIdentifiable
{
    /// <summary>
    ///     Identifier of identifiable object
    /// </summary>
    public Guid Id { get; set; }
}