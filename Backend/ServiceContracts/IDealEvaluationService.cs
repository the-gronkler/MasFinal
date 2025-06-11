using MasFinal.Models;

namespace MasFinal.ServiceContracts;

public interface IDealEvaluationService
{
    Task<bool> EvaluateInitialDealEligibility(Deal deal);
    Task<bool> EvaluateDealEligibilityWithProof(Deal deal, List<int> selectedPoliticians);
    
    /// <summary>
    /// Sets status to auto-rejected for all deals that are in the PreScreening state, and have been proposed more than 1.2 hours ago.
    /// </summary>
    Task<int> CleanUpPreScreeningDealsAsync();
}