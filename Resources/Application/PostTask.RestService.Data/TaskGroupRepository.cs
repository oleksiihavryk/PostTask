using Microsoft.EntityFrameworkCore;
using PostTask.RestService.Data.Exceptions;
using PostTask.RestService.Data.Interfaces;
using PostTask.RestService.Domain;
using Task = System.Threading.Tasks.Task;

namespace PostTask.RestService.Data;
/// <summary>
///     Task group repository service implementation
/// </summary>
public class TaskGroupRepository : BaseRepository, ITaskGroupRepository
{
    /// <summary>
    ///     Create default task group repository
    /// </summary>
    /// <param name="context">
    ///     Database context of system
    /// </param>
    public TaskGroupRepository(PostTaskDatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskGroup>> GetAllByUserIdentifierAsync(string userId)
        => await Task.Run(() => 
            _context.TaskGroups.Where(tg => tg.UserId == userId));
    public async Task<TaskGroup> GetByIdentifierAsync(Guid id)
        => (await _context.TaskGroups
               .FirstOrDefaultAsync(tg => tg.Id == id)) ??
           throw new ObjectNotFoundException(_context.TaskGroups);
    public async Task<bool> CheckIfUserByIdContentThisEntityById(
        string userId, 
        Guid entityId)
        => (await GetAllByUserIdentifierAsync(userId))
            .Select(tg => tg.Id)
            .Contains(entityId);
    public async Task RemoveByIdentifierAsync(Guid id)
    {
        var entity = await GetByIdentifierAsync(id);
        
        _context.TaskGroups.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<TaskGroup> AddAsync(TaskGroup item)
    {
        await _context.TaskGroups.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }
    public async Task<TaskGroup> UpdateAsync(TaskGroup item)
    {
        var dbEntity = await GetByIdentifierAsync(item.Id).ConfigureAwait(false);

        PropertyUpdater.Update(
            updatingEntity: dbEntity,
            entityData: item,
            nameof(TaskGroup.Id));
        await _context.SaveChangesAsync();

        return dbEntity;
    }
}