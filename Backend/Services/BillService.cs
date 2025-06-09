using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

public class BillService(IBillRepository billRepository) : IBillService
{
    public async Task ProposeBillAsync(string name, string description, int proposerId)
    {
        Bill bill = new Bill
        {
            Name = name,
            Description = description,
            ProposerId = proposerId,
            Status = BillStatus.Proposed
        };
        
        await billRepository.AddAsync(bill);
    }

    public async Task SupportBillAsync(int billId, int supporterId)
    {
        await billRepository.SupportBillAsync(billId, supporterId);
    }

    public async Task OpposeBillAsync(int billId, int opposerId)
    {
        await billRepository.OpposeBillAsync(billId, opposerId);
    }
}