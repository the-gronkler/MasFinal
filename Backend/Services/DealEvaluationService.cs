using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

public class DealEvaluationService(
    IPersonRepository personRepository,
    IDealRepository dealRepository
) : IDealEvaluationService
{
    /// <returns>True if the Oligarch is pre-approved, otherwise false.</returns>
    public async Task<bool> EvaluateInitialDealEligibility(Deal deal)
    {

        var oligarch = await personRepository.GetPersonWithDetailsAsync(deal.ProposerId);

        if (oligarch == null || !oligarch.IsOligarch())
            return false;

        double wealth = oligarch.Wealth ?? 0;
        double totalPayroll = oligarch.OwnedBusinesses.Sum(b => b.Workers.Sum(w => w.Wage));
        const double businessValueMultiplier = 1000;

        double totalAssetValue = wealth + (totalPayroll * businessValueMultiplier);

        double requiredValue = deal.DealLevel switch
        {
            1 => 500_000_000, // Requires $500M in assets for the most expensive deals
            2 => 200_000_000, // $200M
            3 => 50_000_000, // $50M
            4 => 10_000_000, // $10M
            5 => 1_000_000, // $1M 
            _ => double.MaxValue // Automatically invalid deal level
        };

        return totalAssetValue >= requiredValue;
    }

    /// <summary>
    /// Evaluates deal eligibility based on a "Confidence Score" calculated from
    /// the influence and past successful dealings with selected reference politicians.
    /// </summary>
    /// <param name="deal">The deal to be evaluated.</param>
    /// <param name="selectedPoliticiansIds">A list of up to 3 Politician IDs provided as proof.</param>
    /// <returns>True if the confidence score meets the required threshold, otherwise false.</returns>
    public async Task<bool> EvaluateDealEligibilityWithProof(Deal deal, List<int> selectedPoliticiansIds)
    {
        var oligarch = await personRepository.GetByIdAsync(deal.ProposerId);
        if (oligarch == null) return false;
        
        var referencePoliticians = (await dealRepository
                .GetPoliticiansDealtWithByOligarchFilteredAsync(deal.ProposerId, selectedPoliticiansIds))
            .ToList();
    
        if (referencePoliticians.Count == 0)
            return false; 
        
        var allPastAcceptedDeals = await dealRepository
            .GetAcceptedDealsBetweenAsync(deal.ProposerId, selectedPoliticiansIds);
        

        double confidenceScore = 0;
        confidenceScore += referencePoliticians.Sum(p => p.InfluenceScore ?? 0);
        confidenceScore += allPastAcceptedDeals.Sum(d => (6 - d.DealLevel) * 2);
        
        double requiredScore = deal.DealLevel switch
        {
            1 => 30, 
            2 => 20,
            3 => 15,
            4 => 10,
            5 => 5,  
            _ => double.MaxValue
        };
    
        return confidenceScore >= requiredScore;
    }
}


