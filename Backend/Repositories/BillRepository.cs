using MasFinal.Models;
using MasFinal.RepositoryContracts;

namespace MasFinal.Repositories;

public class BillRepository(AppDbContext context) 
    : Repository<Bill>(context), IBillRepository
{
    public Task<Bill?> GetBillWithRelationsAsync(int billId)
    {
        throw new NotImplementedException();
    }

    public Task SupportBillAsync(int billId, int politicianId)
    {
        throw new NotImplementedException();
    }

    public Task OpposeBillAsync(int billId, int politicianId)
    {
        throw new NotImplementedException();
    }

    public Task ChangeBillStatusAsync(int billId, BillStatus newStatus)
    {
        throw new NotImplementedException();
    }
}