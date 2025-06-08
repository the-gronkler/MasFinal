using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts.PoliticalOrganisations;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Repositories.PoliticalOrganisations;

public class MovementRepository(AppDbContext context) 
    : Repository<Movement>(context), IMovementRepository
{
    
    public override async Task AddAsync(Movement movement)
    {
        // The  name must be unique across all PoliticalOrganisations.
        if (await _context.PoliticalOrganisations.AnyAsync(po => po.Name == movement.Name))
            throw new InvalidOperationException($"An organization with the name '{movement.Name}' already exists.");
        
        await base.AddAsync(movement);
    }
    
    public async Task AddMovementMemberAsync(int movementId, int politicianId, bool isLeader)
    {
        var politician = await _context.Persons.FindAsync(politicianId) ?? throw new KeyNotFoundException("Politician not found.");
        if (!politician.IsPolitician())
            throw new InvalidOperationException("Only politicians can join movements.");

        var movementExists = await _dbSet.AnyAsync(m => m.OrganisationId == movementId);
        if (!movementExists)
            throw new KeyNotFoundException("Movement not found.");
        
        
        var membership = new MovementMembership
        {
            MovementId = movementId,
            PoliticianId = politicianId,
            IsLeader = isLeader,
            StartDate = DateTime.UtcNow
        };
        await _context.MovementMemberships.AddAsync(membership);
    }

    public async Task EndMovementMembershipAsync(int membershipId)
    {
        var membership = await _context.MovementMemberships.FindAsync(membershipId) ?? throw new KeyNotFoundException("Membership not found.");
        if (membership.EndDate != null) return; // Already ended
        
        membership.EndDate = DateTime.UtcNow;
        _context.MovementMemberships.Update(membership);
    }

    public async Task AddSupportingOrganisationAsync(int movementId, int supportingOrgId)
    {
        var movement = await _dbSet.Include(m => m.SupportedBy).FirstOrDefaultAsync(m => m.OrganisationId == movementId) 
                       ?? throw new KeyNotFoundException($"Movement with ID {movementId} not found.");
        var supportingOrg = await _context.PoliticalOrganisations.FindAsync(supportingOrgId) 
                            ?? throw new KeyNotFoundException($"Supporting organisation with ID {supportingOrgId} not found.");

        if (movement.OrganisationId == supportingOrg.OrganisationId)
            throw new InvalidOperationException("An organisation cannot support itself.");

        if (!movement.SupportedBy.Contains(supportingOrg))
            movement.SupportedBy.Add(supportingOrg);
        
    }

    public async Task RemoveSupportingOrganisationAsync(int movementId, int supportingOrgId)
    {
        var movement = await _dbSet
                           .Include(m => m.SupportedBy)
                           .FirstOrDefaultAsync(m => m.OrganisationId == movementId) 
                       ?? throw new KeyNotFoundException($"Movement with ID {movementId} not found.");
        var supportingOrg = movement.SupportedBy
            .FirstOrDefault(o => o.OrganisationId == supportingOrgId);
        
        if (supportingOrg != null)
            movement.SupportedBy.Remove(supportingOrg);
    }
}