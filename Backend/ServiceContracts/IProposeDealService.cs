using MasFinal.Models;

namespace MasFinal.ServiceContracts;

/// <summary>
/// Service for proposing deals between oligarchs and politicians.
/// Is intended to be exposed to the GUI, and other possible clients.
/// </summary>
public interface IProposeDealService
{
    Task<IEnumerable<Person>> GetOligarchsAsync();
    Task<IEnumerable<Person>> GetPoliticiansAsync();
    
    Task<IEnumerable<Deal>> GetAllDealsFor(int personId);
    
    Task<IEnumerable<Deal>> GetDealBetweenAsync(int oligarchId, int politicianId);

    /// <returns>returns politicians with whom the provided oligarch had deals</returns>
    Task<IEnumerable<Person>> GetPreviouslyDealtPoliticiansAsync(int oligarchId);
    
    
    Task<Deal> TryProposeDealAsync(int oligarchId, int politicianId, string dealDescription, int dealLevel);
    Task<Deal> ProveDealEligibilityAsync(int dealId, List<int> selectedPoliticians);

}