using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

// for ui
public class ProposeDealService(
    IPersonRepository personRepository,
    IDealRepository dealRepository,
    IDealEvaluationService dealEvaluationService
    ) : IProposeDealService
{
    public async Task<IEnumerable<Person>> GetOligarchsAsync()
    {
        return await personRepository.FindAsync(p => p.Types.Contains(PersonType.Oligarch));
    }

    public async Task<IEnumerable<Person>> GetPoliticiansAsync()
    {
        return await personRepository.FindAsync(p => p.Types.Contains(PersonType.Politician));
    }

    public async Task<IEnumerable<Deal>> GetAllDealsFor(int personId)
    {
        return await dealRepository.GetDealsForPerson(personId);
    }

    public async Task<IEnumerable<Deal>> GetDealBetweenAsync(int oligarchId, int politicianId)
    {
        if (oligarchId <= 0 || politicianId <= 0)
            throw new ArgumentException("Oligarch and Politician IDs must be greater than zero.");

        return await dealRepository.FindAsync(d => d.ProposerId == oligarchId && d.RecipientId == politicianId);
    }

    public async Task<IEnumerable<Person>> GetPreviouslyDealtPoliticiansAsync(int oligarchId)
    {
        if (oligarchId <= 0)
            throw new ArgumentException("Oligarch ID must be greater than zero.");
        
        return await dealRepository.GetPoliticiansDealtWithByOligarchAsync(oligarchId);
    }

    
    public async Task<Deal> TryProposeDealAsync(int oligarchId, int politicianId, string dealDescription, int dealLevel)
    {
        if (oligarchId <= 0 || politicianId <= 0)
            throw new ArgumentException("Oligarch and Politician IDs must be greater than zero.");

        if (string.IsNullOrWhiteSpace(dealDescription))
            throw new ArgumentException("Deal description cannot be empty.");
        
        var deal = new Deal()
        {
            ProposerId = oligarchId,
            RecipientId = politicianId,
            Description = dealDescription,
            DealLevel = dealLevel,
            DateProposed = DateTime.UtcNow,
            Status = DealStatus.PreScreening
        };
        
        deal = await dealRepository.AddAsync(deal);

        bool didEvalSucceed = await dealEvaluationService.EvaluateInitialDealEligibility(deal);

        if (!didEvalSucceed) 
            return deal;
        
        deal.PreApprove();
        // dealRepository.Update(deal);
        await dealRepository.SaveChangesAsync();

        return deal;
    }
    
    public async Task<Deal> ProveDealEligibilityAsync(int dealId, List<int> selectedPoliticians)
    {
        if (dealId <= 0)
            throw new ArgumentException("Deal ID must be greater than zero.");

        if (selectedPoliticians == null || selectedPoliticians.Count == 0)
            throw new ArgumentException("At least one politician must be selected.");
        
        if (selectedPoliticians.Count > 3)
            throw new ArgumentException("You cannot select more than three politicians for proof.");

        var deal = await dealRepository.GetByIdAsync(dealId);
        if (deal == null)
            throw new KeyNotFoundException($"Deal with ID {dealId} not found.");
        if (deal.Status != DealStatus.PreScreening)
            throw new InvalidOperationException($"Cannot prove eligibility for a deal that is not in the 'PreScreening' state. Current state: {deal.Status}");

        bool didEvalSucceed = await dealEvaluationService.EvaluateDealEligibilityWithProof(deal, selectedPoliticians);
        
        if (didEvalSucceed)
            deal.PreApprove();
        else
            deal.AutoReject();

        dealRepository.Update(deal);
        await dealRepository.SaveChangesAsync();

        return deal;
    }
    
    
    /// <summary>
    /// resumes evaluation process for deals in PreScreening status.
    /// Does not allow for second level evaluation where oligarchs can additionally 'prove' eligibility,
    /// the decision from 1st level evaluation is final.
    /// Should be run periodically to ensure no deals are left in PreScreening indefinitely.
    /// </summary>
    /// <returns></returns>
    public async Task ResumePreScreeningDealsAsync()
    {
        var orphanedDeals = await dealRepository.FindAsync(d => 
            d.Status == DealStatus.PreScreening && 
            (DateTime.UtcNow - d.DateProposed) > TimeSpan.FromHours(1.2)
        );

        var deals = orphanedDeals.ToList();
        if (deals.Count == 0)
            return;

        foreach (var deal in deals)
        {
            deal.AutoReject();
            dealRepository.Update(deal);
        }

        await dealRepository.SaveChangesAsync();
    }
}