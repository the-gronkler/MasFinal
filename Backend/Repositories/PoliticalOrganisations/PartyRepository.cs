using MasFinal.Data;
using MasFinal.Models;
using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts.PoliticalOrganisations;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Repositories.PoliticalOrganisations;

public class PartyRepository (AppDbContext context) 
    : Repository<Party>(context), IPartyRepository
{
    /// <summary>
    /// Adds a new Party, enforcing creation-time constraints.
    /// A Party must be created with a unique name and at least one member.
    /// </summary>
    public override async Task<Party> AddAsync(Party party)
    {
        // A Party must have at least 1 member (as per user correction).
        if (party.Memberships == null || party.Memberships.Count == 0)
            throw new InvalidOperationException("A Party must be created with at least one member.");
        

        // The Party's name must be unique across all PoliticalOrganisations.
        if (await _context.PoliticalOrganisations.AnyAsync(po => po.Name == party.Name))
            throw new InvalidOperationException($"An organization with the name '{party.Name}' already exists.");
        
        return await base.AddAsync(party);
    }

    public async Task AddPartyMemberAsync(int partyId, int politicianId, PartyPosition position)
    {
        var politician = await _context.Persons.FindAsync(politicianId) ?? throw new KeyNotFoundException("Politician not found.");
        if (!politician.IsPolitician())
            throw new InvalidOperationException("Only politicians can join parties.");

        var partyExists = await _dbSet.AnyAsync(p => p.OrganisationId == partyId);
        if (!partyExists)
            throw new KeyNotFoundException("Party not found.");

        // A politician can be a member of maximum 1 Party at a time.
        var hasActiveMembership = await _context.PartyMemberships
            .AnyAsync(pm => pm.PoliticianId == politicianId && pm.EndDate == null);
        if (hasActiveMembership)
            throw new InvalidOperationException("This politician is already an active member of a party.");
        
        var membership = new PartyMembership
        {
            PartyId = partyId,
            PoliticianId = politicianId,
            Position = position,
            StartDate = DateTime.UtcNow
        };
        await _context.PartyMemberships.AddAsync(membership);
    }

    public async Task EndPartyMembershipAsync(int membershipId)
    {
        var membership = await _context.PartyMemberships.FindAsync(membershipId) ?? throw new KeyNotFoundException("Membership not found.");
        if (membership.EndDate != null) return; // Already ended

        // A Party must have at least 1 member.
        var activeMemberCount = await _context.PartyMemberships
            .CountAsync(pm => pm.PartyId == membership.PartyId && pm.EndDate == null);
        if (activeMemberCount <= 1)
            throw new InvalidOperationException("Cannot remove the last member of a party.");

        membership.EndDate = DateTime.UtcNow;
        _context.PartyMemberships.Update(membership);
    }
}