using MasFinal.Models;

namespace MasFinal.RepositoryContracts;

public interface IBillRepository : IRepository<Bill>
{
    Task<Bill?> GetBillWithRelationsAsync(int billId);
    Task SupportBillAsync(int billId, int politicianId);
    Task OpposeBillAsync(int billId, int politicianId);
    Task ChangeBillStatusAsync(int billId, BillStatus newStatus);
}