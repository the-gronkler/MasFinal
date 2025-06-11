using MasFinal.Models;

namespace MasFinal.ServiceContracts;

public interface IProposeDealService
{
    // for oligarch:
    Task<IEnumerable<Person>> GetOligarchsAsync();
    Task<IEnumerable<Person>> GetPoliticiansAsync();
    
    Task<IEnumerable<Deal>> GetAllDealsFor(int personId);
    
    Task<IEnumerable<Deal>> GetDealHistoryAsync(int oligarchId, int politicianId);

    /// <returns>returns politicians with whom the provided oligarch had deals</returns>
    Task<IEnumerable<Person>> GetPreviouslyDealtPoliticiansAsync(int oligarchId);
    
    
    Task<Deal> PreApproveDealAsync(int oligarchId, int politicianId, string dealDescription, int dealLevel);
    Task<Deal> ProveDealEligibilityAsync(int dealId, List<int> selectedPoliticians);

    Task ResumePreScreeningDealsAsync();
}