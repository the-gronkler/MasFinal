using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Data;

public interface IDataDisplayer
{
    Task DisplayAsync();
}

public class DataDisplayer(
    IPersonRepository personRepository,
    IDealRepository dealRepository,
    IBillRepository billRepository,
    IPartyRepository partyRepository,
    IMovementRepository movementRepository,
    IBusinessRepository businessRepository,
    IWorkerRepository workerRepository
) : IDataDisplayer
{
    public async Task DisplayAsync()
    {
        throw new NotImplementedException();
    }
}