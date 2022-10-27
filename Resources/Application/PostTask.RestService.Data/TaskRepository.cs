using Microsoft.EntityFrameworkCore;
using PostTask.RestService.Data.Exceptions;
using PostTask.RestService.Data.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace PostTask.RestService.Data;

/// <summary>
///     Task repository
/// </summary>
public class TaskRepository : BaseRepository, ITaskRepository
{
    /// <summary>
    ///     Create default task repository service
    /// </summary>
    /// <param name="context">
    ///     Database context of system
    /// </param>
    public TaskRepository(PostTaskDatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Task>> GetAllByUserIdentifierAsync(string userId)
        => await Task.Run(() =>
            _context.Tasks.Where(t => t.UserId == userId));
    public async Task<Domain.Task> GetByIdentifierAsync(Guid id)
        => (await _context.Tasks
               .FirstOrDefaultAsync(t => t.Id == id)) ??
           throw new ObjectNotFoundException(_context.Tasks);
    public async Task<bool> CheckIfUserByIdContentThisEntityById(string userId, Guid entityId)
        => (await GetAllByUserIdentifierAsync(userId))
            .Select(t => t.Id)
            .Contains(entityId);
    public async Task RemoveByIdentifierAsync(Guid id)
    {
        var entity = await GetByIdentifierAsync(id);
        
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<Domain.Task> AddAsync(Domain.Task item)
    {
        await _context.Tasks.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }
    public async Task<Domain.Task> UpdateAsync(Domain.Task item)
    {
        var dbEntity = await GetByIdentifierAsync(item.Id);

        PropertyUpdater.Update(
            updatingEntity: dbEntity, 
            entityData: item, 
            nameof(Domain.Task.Id));
        await _context.SaveChangesAsync();

        return dbEntity;
    }
}