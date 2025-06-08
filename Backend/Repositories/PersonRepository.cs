using MasFinal.Models;
using MasFinal.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Repositories;


public class PersonRepository(AppDbContext context) 
    : Repository<Person>(context), IPersonRepository
{

    public override async Task AddAsync(Person person)
    {
        if (person.Types == null || person.Types.Count == 0)
            throw new InvalidOperationException("A person must be created with at least one role (Politician or Oligarch).");
        
        await base.AddAsync(person);
    }
    
    public async Task<Person?> GetPersonWithDetailsAsync(int personId)
    {
        return await _dbSet
            .Include(p => p.OwnedBusinesses)
            .ThenInclude(b => b.Workers)
            .Include(p => p.PartyMemberships)
            .Include(p => p.MovementMemberships)
            .FirstOrDefaultAsync(p => p.PersonId == personId);
    }

    public async Task BecomePoliticianAsync(int personId, int influenceScore)
    {
        var person = await GetByIdAsync(personId) ?? throw new KeyNotFoundException($"Person with ID {personId} not found.");
        
        if (person.Types.Contains(PersonType.Politician))
            throw new InvalidOperationException("This person is already a Politician.");
        

        person.Types.Add(PersonType.Politician);
        person.InfluenceScore = influenceScore;
        Update(person);
    }

    public async Task BecomeOligarchAsync(int personId, double wealth)
    {
        var person = await GetByIdAsync(personId) ?? throw new KeyNotFoundException($"Person with ID {personId} not found.");
        if (person.Types.Contains(PersonType.Oligarch))
            throw new InvalidOperationException("This person is already an Oligarch.");
        

        person.Types.Add(PersonType.Oligarch);
        person.Wealth = wealth;
        Update(person);
    }

    public async Task QuitAsPoliticianAsync(int personId)
    {
        var person = await _dbSet
            .Include(p => p.PartyMemberships)
            .Include(p => p.MovementMemberships)
            .Include(p => p.BillsProposed) // Eager load bills to nullify proposer
            .FirstOrDefaultAsync(p => p.PersonId == personId) ?? throw new KeyNotFoundException($"Person with ID {personId} not found.");

        if (!person.Types.Contains(PersonType.Politician)) return; // Already not a politician

        if (person.Types.Count == 1)
            throw new InvalidOperationException("Cannot remove the last role from a person. A person must be either a Politician or an Oligarch.");
        

        // Lose role-specific data
        person.InfluenceScore = null;
        
        // End active memberships
        var now = DateTime.UtcNow;
        foreach (var membership in person.PartyMemberships.Where(m => m.EndDate == null))
            membership.EndDate = now;
        
        foreach (var membership in person.MovementMemberships.Where(m => m.EndDate == null))
            membership.EndDate = now;
        
        foreach (var bill in person.BillsProposed)
            bill.ProposerId = null;
        

        person.Types.Remove(PersonType.Politician);
        Update(person);
    }

    public async Task QuitAsOligarchAsync(int personId)
    {
        var person = await _dbSet
            .Include(p => p.OwnedBusinesses)
            .FirstOrDefaultAsync(p => p.PersonId == personId) ?? throw new KeyNotFoundException($"Person with ID {personId} not found.");

        if (!person.Types.Contains(PersonType.Oligarch)) return; // Already not an oligarch

        if (person.Types.Count == 1)
            throw new InvalidOperationException("Cannot remove the last role from a person. A person must be either a Politician or an Oligarch.");
        

        // Lose role-specific data
        person.Wealth = null;
        _context.Businesses.RemoveRange(person.OwnedBusinesses);
        
        person.Types.Remove(PersonType.Oligarch);
        Update(person);
    }
    
}
