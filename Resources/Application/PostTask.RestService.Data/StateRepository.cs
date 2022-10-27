using Microsoft.EntityFrameworkCore;
using PostTask.RestService.Data.Exceptions;
using PostTask.RestService.Data.Interfaces;
using PostTask.RestService.Domain;
using Task = System.Threading.Tasks.Task;

namespace PostTask.RestService.Data;
/// <summary>
///     State repository
/// </summary>
public class StateRepository : BaseRepository, IStateRepository
{
    /// <summary>
    ///     Create default state repository service
    /// </summary>
    /// <param name="context">
    ///     Repository database context
    /// </param>
    public StateRepository(PostTaskDatabaseContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<State>> GetAllByUserIdentifierAsync(string userId)
        => await Task.Run(() =>
            _context.UserStates
                .Where(us => us.UserId == userId));
    public async Task<State> GetByIdentifierAsync(Guid id)
        => (await _context.States
               .FirstOrDefaultAsync(s => s.Id == id)) ?? 
           throw new ObjectNotFoundException(_context.States);
    public async Task<bool> CheckIfUserByIdContentThisEntityById(string userId, Guid entityId)
        => (await GetAllByUserIdentifierAsync(userId))
            .Select(s => s.Id)
            .Contains(entityId);
    public async Task<IEnumerable<State>> GetBasicStateWithUserIdentifierStatesAsync(string userId)
    {
        var entities = (await GetAllByUserIdentifierAsync(userId)).ToList();
        var baseStates = _context.States.Where(s => !(s is UserState)).ToArray();

        entities.AddRange(baseStates);

        return entities;
    }
    public async Task<State> AddAsync(State item)
    {
        await _context.States.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }
    public async Task RemoveByIdentifierAsync(Guid id)
    {
        var entity = await GetByIdentifierAsync(id);
        
        _context.States.Remove(entity);
        await _context.SaveChangesAsync();
    }
}