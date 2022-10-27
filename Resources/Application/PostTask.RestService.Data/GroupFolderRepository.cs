using Microsoft.EntityFrameworkCore;
using PostTask.RestService.Data.Exceptions;
using PostTask.RestService.Data.Interfaces;
using PostTask.RestService.Domain;
using Task = System.Threading.Tasks.Task;

namespace PostTask.RestService.Data;
/// <summary>
///     Group folder repository base implementation
/// </summary>
public class GroupFolderRepository : IGroupFolderRepository
{
    /// <summary>
    ///     Database context
    /// </summary>
    private readonly PostTaskDatabaseContext _context;

    /// <summary>
    ///     Create default groups folder repository service
    /// </summary>
    /// <param name="context">
    ///     Database context
    /// </param>
    public GroupFolderRepository(PostTaskDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GroupFolder>> GetAllByUserIdentifierAsync(string userId)
        => await Task.Run(() =>
            _context.GroupFolders.Where(gf => gf.UserId == userId));
    public async Task<GroupFolder> GetByIdentifierAsync(Guid id)
        => (await _context.GroupFolders
               .FirstOrDefaultAsync(gf => gf.Id == id)) ??
           throw new ObjectNotFoundException(_context.GroupFolders);
    public async Task<bool> CheckIfUserByIdContentThisEntityById(
        string userId, 
        Guid entityId)
        => (await GetAllByUserIdentifierAsync(userId))
            .Select(gf => gf.Id)
            .Contains(entityId);
    public async Task RemoveByIdentifierAsync(Guid id)
    {
        var entity = await GetByIdentifierAsync(id);
        
        _context.GroupFolders.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<GroupFolder> AddAsync(GroupFolder item)
    {
        await _context.GroupFolders.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }
    public async Task<GroupFolder> UpdateAsync(GroupFolder item)
    {
        var dbEntity = await GetByIdentifierAsync(item.Id);

        BaseRepository.PropertyUpdater.Update(
            updatingEntity: dbEntity,
            entityData: item,
            exceptParamNames: nameof(GroupFolder.Id));
        await _context.SaveChangesAsync();

        return dbEntity;
    }
}