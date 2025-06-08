using MasFinal.Models;
using MasFinal.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Repositories;



public class BillRepository(AppDbContext context) 
    : Repository<Bill>(context), IBillRepository
{
    /// <summary>
    /// Adds a new bill, ensuring the proposer is a politician.
    /// </summary>
    public override async Task AddAsync(Bill bill)
    {
        if (bill.ProposerId.HasValue)
        {
            var proposer = await _context.Persons.FindAsync(bill.ProposerId.Value);
            if (proposer == null || !proposer.IsPolitician())
                throw new InvalidOperationException("The proposer of a bill must be a politician.");
            
        }
        await base.AddAsync(bill);
    }

    public async Task<Bill?> GetBillWithRelationsAsync(int billId)
    {
        return await _dbSet
            .Include(b => b.Proposer)
            .Include(b => b.Supporters)
            .Include(b => b.Opposers)
            .FirstOrDefaultAsync(b => b.BillId == billId);
    }

    public async Task SupportBillAsync(int billId, int politicianId)
    {
        var bill = await GetBillWithRelationsAsync(billId) ?? throw new KeyNotFoundException("Bill not found.");
        var politician = await _context.Persons.FindAsync(politicianId) ?? throw new KeyNotFoundException("Politician not found.");
        
        if(!politician.IsPolitician())
            throw new InvalidOperationException("Only politicians can support bills.");

        // XOR  check
        if (bill.Opposers.Any(p => p.PersonId == politicianId))
            throw new InvalidOperationException("This politician already opposes the bill and cannot support it.");
        
        
        if (bill.Supporters.All(p => p.PersonId != politicianId))
            bill.Supporters.Add(politician);
        
    }

    public async Task OpposeBillAsync(int billId, int politicianId)
    {
        var bill = await GetBillWithRelationsAsync(billId) ?? throw new KeyNotFoundException("Bill not found.");
        var politician = await _context.Persons.FindAsync(politicianId) ?? throw new KeyNotFoundException("Politician not found.");
        
        if(!politician.IsPolitician())
            throw new InvalidOperationException("Only politicians can oppose bills.");

        // XOR Constraint check
        if (bill.Supporters.Any(p => p.PersonId == politicianId))
            throw new InvalidOperationException("This politician already supports the bill and cannot oppose it.");
        
        
        if (bill.Opposers.All(p => p.PersonId != politicianId))
            bill.Opposers.Add(politician);
    }


    public async Task ChangeBillStatusAsync(int billId, BillStatus newStatus)
    {
        var bill = await GetByIdAsync(billId) ?? throw new KeyNotFoundException("Bill not found.");
        bill.Status = newStatus;
        Update(bill);
    }
}
