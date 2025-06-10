using System.Collections;
using MasFinal.Data;
using MasFinal.Models;
using MasFinal.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Repositories;


public class DealRepository(AppDbContext context)
    : Repository<Deal>(context), IDealRepository
{
    /// <summary>
    /// Adds a new deal, performing validation checks.
    /// </summary>
    public override async Task<Deal> AddAsync(Deal deal)
    {
        if (deal.ProposerId == deal.RecipientId)
            throw new InvalidOperationException("A person cannot propose a deal to themselves.");

        // Role validation
        var proposer = await _context.Persons.FindAsync(deal.ProposerId) ??
                       throw new KeyNotFoundException("Proposer not found.");
        var recipient = await _context.Persons.FindAsync(deal.RecipientId) ??
                        throw new KeyNotFoundException("Recipient not found.");

        if (!proposer.IsOligarch())
            throw new InvalidOperationException($"The proposer (ID: {proposer.PersonId}) must be an Oligarch.");


        if (!recipient.IsPolitician())
            throw new InvalidOperationException($"The recipient (ID: {recipient.PersonId}) must be a Politician.");


        return await base.AddAsync(deal);
    }

    public async Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchAsync(int oligarchId)
    {
        return await _context.Deals
            .Where(d => d.ProposerId == oligarchId)
            .Select(d => d.Recipient)
            .Distinct()
            .ToListAsync();
    }


    // deal repository
    public async Task<IEnumerable<Person>> GetPoliticiansDealtWithByOligarchFilteredAsync(
        int oligarchId, List<int> selectedPoliticiansIds)
    {
        return await _context.Deals
            .Where(d => d.ProposerId == oligarchId)
            .Select(d => d.Recipient)
            .Where(p => selectedPoliticiansIds.Contains(p.PersonId))
            .Distinct()
            .ToListAsync();
    }


    public async Task<IEnumerable<Deal>> GetAcceptedDealsBetweenAsync(int oligarchId, List<int> politicianIds)
    {
        return await _dbSet
            .Where(d =>
                d.ProposerId == oligarchId &&
                politicianIds.Contains(d.RecipientId) &&
                d.Status == DealStatus.Accepted)
            .ToListAsync();
    }
}
