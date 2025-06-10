using System.Collections;
using MasFinal.Models;

namespace MasFinal.RepositoryContracts;

public interface IDealRepository : IRepository<Deal>
{
    Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchAsync(int oligarchId);
    Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchFilteredAsync(
        int dealProposerId, List<int> selectedPoliticiansIds);
    
    Task<IEnumerable<Deal>> GetAcceptedDealsBetweenAsync(int oligarchId, List<int> politicianIds);
}