using PostTask.RestService.Domain;

namespace PostTask.RestService.Data.Interfaces;

/// <summary>
///     State repository interface, implementation of IMinimalRepository by type state
///     but with some addition.
/// </summary>
public interface IStateRepository : IMinimalRepository<State>
{
    /// <summary>
    ///     Get basic state and state by current user identifier
    /// </summary>
    /// <param name="userId">
    ///     User identifier of searching states
    /// </param>
    /// <returns>
    ///     Returns find base and by user id states
    /// </returns>
    Task<IEnumerable<State>> GetBasicStateWithUserIdentifierStatesAsync(string userId);
}