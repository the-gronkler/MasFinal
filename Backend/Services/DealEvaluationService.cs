using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

public class DealEvaluationService(
    IPersonRepository personRepository,
    IDealRepository dealRepository
    ) : IDealEvaluationService 
{
    public Task<bool> EvaluateInitialDealEligibility(Deal deal)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EvaluateDealEligibilityWithProof(Deal deal, List<int> selectedPoliticians)
    {
        throw new NotImplementedException();
    }
}