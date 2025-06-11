using System.Collections;
using MasFinal.Models;

namespace MasFinal.RepositoryContracts;

public interface IDealRepository : IRepository<Deal>
{

    /// <returns>Deals in which this person participated in ANY capacity</returns>
    Task<IEnumerable<Deal>> GetDealsForPerson(int personId);
    
    /// <returns>Deals in which this person participated as Recipient (even if they arent a politician ANYMORE)</returns>
    Task<IEnumerable<Deal>> GetDealsForPolitician(int politicianId);
    
    /// <returns>Deals in which this person participated as Proposer (even if they arent an oligarch ANYMORE)</returns>
    Task<IEnumerable<Deal>> GetDealsForOligarch(int oligarchId);
    
    Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchAsync(int oligarchId);
    Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchFilteredAsync(
        int dealProposerId, List<int> selectedPoliticiansIds);
    
    Task<IEnumerable<Deal>> GetAcceptedDealsBetweenAsync(int oligarchId, List<int> politicianIds);
}