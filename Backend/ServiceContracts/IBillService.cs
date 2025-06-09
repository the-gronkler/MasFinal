namespace MasFinal.ServiceContracts;

public interface IBillService
{
    Task ProposeBillAsync(string name, string description, int proposerId);
    
    
    Task SupportBillAsync(
        int billId,
        int supporterId
    );
    
    Task OpposeBillAsync(
        int billId,
        int opposerId
    );
    
    
    
    
}