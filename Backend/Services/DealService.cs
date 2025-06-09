using MasFinal.RepositoryContracts;
using MasFinal.ServiceContracts;

namespace MasFinal.Services;

public class DealService(
    IPersonRepository personRepository,
    IDealRepository dealRepository
    ) : IDealService
{
    
}