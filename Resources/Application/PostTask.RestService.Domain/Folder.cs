using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using PostTask.RestService.Domain.Interfaces;

namespace PostTask.RestService.Domain;

/// <summary>
///     Folder for contain identifiable elements
/// </summary>
/// <typeparam name="T">Its type of containable object</typeparam>
public abstract class Folder<T> : ICollection<T>, IIdentifiable
    where T : IIdentifiable
{
    /// <summary>
    ///     List of contained elements
    /// </summary>
    private readonly List<T> _list = new List<T>();

    /// <summary>
    ///     Count of contained elements in folder
    /// </summary>
    [NotMapped] public int Count => _list.Count;
    /// <summary>
    ///     Is folder a readonly or not
    /// </summary>
    [NotMapped] public virtual bool IsReadOnly => false;

    /// <summary>
    ///     Folder identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    ///     Folder name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Folder description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    ///     Folder user identifier 
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    ///     Add item into folder
    /// </summary>
    /// <param name="item">
    ///     Item which has been added into folder
    /// </param>
    public void Add(T item)
        => _list.Add(item);
    /// <summary>
    ///     Remove all items from folder
    /// </summary>
    public void Clear()
        => _list.Clear();
    /// <summary>
    ///     Check on contains item in folder
    /// </summary>
    /// <param name="item">
    ///     Checked item
    /// </param>
    /// <returns>
    ///     Returns result of checking
    /// </returns>
    public bool Contains(T item)
        => _list.Contains(item);
    /// <summary>
    ///     Copy a content of folder into chosen array
    /// </summary>
    /// <param name="array">
    ///     Array into which content has been copy
    /// </param>
    /// <param name="arrayIndex">
    ///     Array index
    /// </param>
    public void CopyTo(T[] array, int arrayIndex)
        => _list.CopyTo(array, arrayIndex);
    /// <summary>
    ///     Remove item from folder
    /// </summary>
    /// <param name="item">
    ///     Item what was be removed from folder
    /// </param>
    /// <returns>
    ///     Returns true if item is removed from folder, otherwise false
    /// </returns>
    public bool Remove(T item)
        => _list.Remove(item);

    /// <summary>
    ///     Method for get enumerator from this collection
    /// </summary>
    /// <returns>
    ///     Enumerator of folder
    /// </returns>
    public IEnumerator<T> GetEnumerator()
        => _list.GetEnumerator();
    /// <summary>
    ///     Base non-generic IEnumerable implementation
    /// </summary>
    /// <returns>
    ///     Generic enumerator
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}