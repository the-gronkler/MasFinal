using System.Collections;
using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

public class DecideDealService(
    IDealRepository dealRepository
    ) : IDecideDealService
{
    public async Task<IEnumerable> GetPendingDealsAsync(int personId)
    {
        if (personId <= 0)
            throw new ArgumentException("Person ID must be greater than zero.");
        return await dealRepository.FindAsync(d => d.RecipientId == personId && d.Status == DealStatus.PendingDecision);
        
    }

    public async Task AcceptDealAsync(int dealId)
    {
        if (dealId <= 0)
            throw new ArgumentException("Deal ID must be greater than zero.");

        var deal = await dealRepository.GetByIdAsync(dealId);
        if (deal == null)
            throw new KeyNotFoundException("Deal not found.");

        deal.Accept();
        dealRepository.Update(deal);

        await dealRepository.SaveChangesAsync();
    }

    public async Task RejectDealAsync(int dealId)
    {
        if (dealId <= 0)
            throw new ArgumentException("Deal ID must be greater than zero.");

        var deal = await dealRepository.GetByIdAsync(dealId);
        if (deal == null)
            throw new KeyNotFoundException("Deal not found.");

        deal.Decline();
        dealRepository.Update(deal);

        await dealRepository.SaveChangesAsync();
    }
    
}