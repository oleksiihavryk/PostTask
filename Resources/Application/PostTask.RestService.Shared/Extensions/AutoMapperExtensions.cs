using AutoMapper;

namespace PostTask.RestService.Shared.Extensions;
/// <summary>
///     AutoMapper extension methods
/// </summary>
public static class AutoMapperExtensions
{
    /// <summary>
    ///     Create a double linked mapping between T1 and T2
    /// </summary>
    /// <typeparam name="T1">
    ///     First mapped type
    /// </typeparam>
    /// <typeparam name="T2">
    ///     Second mapped type
    /// </typeparam>
    /// <param name="exp">
    ///     Mapper configuration expression
    /// </param>
    public static void CreateDoubleLinkedMap<T1, T2>(this IMapperConfigurationExpression exp)
    {
        exp.CreateMap<T1, T2>();
        exp.CreateMap<T2, T1>();
    }
}