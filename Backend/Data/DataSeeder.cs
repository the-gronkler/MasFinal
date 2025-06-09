using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Data;

public interface IDataSeeder
{
    Task SeedAsync();
}

public class DataSeeder(
    IPersonRepository personRepository,
    IDealRepository dealRepository,
    IBillRepository billRepository,
    IPartyRepository partyRepository,
    IMovementRepository movementRepository,
    IBusinessRepository businessRepository,
    IWorkerRepository workerRepository
) : IDataSeeder
{
    public async Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}