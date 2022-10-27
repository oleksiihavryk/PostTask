using System.Runtime.Serialization;

namespace PostTask.RestService.Shared.Extensions;
/// <summary>
///     Serialization extensions
/// </summary>
public static class SerializationExtensions
{
    /// <summary>
    ///     Get serialized value of chosen type
    /// </summary>
    /// <typeparam name="T">
    ///     Serialized value type
    /// </typeparam>
    /// <param name="info">
    ///     Serialization info object (contain data about all serialized fields of object)
    /// </param>
    /// <param name="serializationKey">
    ///     Serialization key for get access to serialized part of object
    /// </param>
    /// <returns>
    ///     Returns serialized object
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    ///     An error has occurred when serialization key is not found in serialization info object
    /// </exception>
    public static T GetValue<T>(this SerializationInfo info, string serializationKey)
        => (T)(info.GetValue(serializationKey, typeof(T)) ?? 
               throw new KeyNotFoundException(
                   "Serialization key is not found in serialization info object"));
}