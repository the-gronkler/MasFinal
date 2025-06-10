using MasFinal.Models;

namespace MasFinal.ServiceContracts;

public interface IOligarchDealService
{
    // for oligarch:
    Task<IEnumerable<Person>> GetOligarchsAsync();
    Task<IEnumerable<Person>> GetPoliticiansAsync();
    Task<IEnumerable<Deal>> GetDealHistoryAsync(int oligarchId, int politicianId);
    Task<Deal> PreApproveDealAsync(int oligarchId, int politicianId, string dealDescription, int dealLevel);
    Task<IEnumerable<Person>> GetPreviouslyDealtPoliticiansAsync(int oligarchId);
    Task<Deal> ProveDealEligibilityAsync(int dealId, List<int> selectedPoliticians);

    Task ResumePreScreeningDealsAsync();
}