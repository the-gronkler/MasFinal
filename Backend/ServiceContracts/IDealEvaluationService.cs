using MasFinal.Models;

namespace MasFinal.ServiceContracts;

public interface IDealEvaluationService
{
    Task<bool> EvaluateInitialDealEligibility(Deal deal);
    Task<bool> EvaluateDealEligibilityWithProof(Deal deal, List<int> selectedPoliticians);
}