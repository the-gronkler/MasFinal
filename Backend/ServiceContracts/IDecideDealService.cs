using System.Collections;

namespace MasFinal.ServiceContracts;

public interface IDecideDealService
{
    Task<IEnumerable> GetPendingDealsAsync(int personId);
    Task AcceptDealAsync(int dealId);
    Task RejectDealAsync(int dealId);
    
}