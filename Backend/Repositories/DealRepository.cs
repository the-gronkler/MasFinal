using MasFinal.Data;
using MasFinal.Models;
using MasFinal.RepositoryContracts;

namespace MasFinal.Repositories;


public class DealRepository(AppDbContext context) 
    : Repository<Deal>(context), IDealRepository
{
    /// <summary>
    /// Adds a new deal, performing validation checks.
    /// </summary>
    public override async Task AddAsync(Deal deal)
    {
        if (deal.ProposerId == deal.RecipientId)
            throw new InvalidOperationException("A person cannot propose a deal to themselves.");
        
        // Role validation
        var proposer = await _context.Persons.FindAsync(deal.ProposerId) ?? throw new KeyNotFoundException("Proposer not found.");
        var recipient = await _context.Persons.FindAsync(deal.RecipientId) ?? throw new KeyNotFoundException("Recipient not found.");

        if (!proposer.IsOligarch())
            throw new InvalidOperationException($"The proposer (ID: {proposer.PersonId}) must be an Oligarch.");
        

        if (!recipient.IsPolitician())
            throw new InvalidOperationException($"The recipient (ID: {recipient.PersonId}) must be a Politician.");
        

        await base.AddAsync(deal);
    }
}
