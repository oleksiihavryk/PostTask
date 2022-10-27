using Microsoft.Extensions.DependencyInjection;
using PostTask.RestService.Domain;
using PostTask.RestService.Shared.DataTransferObjects;
using Task = PostTask.RestService.Domain.Task;

namespace PostTask.RestService.Shared.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Add auto mapper into DI container
    /// </summary>
    /// <param name="services">
    ///     DI Container and service features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAutoMapperWithApplicationConfiguration(
        this IServiceCollection services)
    {
        services.AddAutoMapper(opt =>
        {
            opt.CreateDoubleLinkedMap<GroupFolder, GroupFolderDto>();
            opt.CreateDoubleLinkedMap<Domain.Task, TaskDto>();
            opt.CreateDoubleLinkedMap<TaskGroup, TaskGroupDto>();
            opt.CreateDoubleLinkedMap<TaskItem, TaskItemDto>();
            opt.CreateDoubleLinkedMap<State, StateDto>();
            opt.CreateDoubleLinkedMap<UserState, UserStateDto>();
        });
        return services;
    }
}