using MasFinal.Models.Businesses;
using MasFinal.RepositoryContracts.Businesses;

namespace MasFinal.Repositories.Businesses;

public class WorkerRepository(AppDbContext context) 
    : Repository<Worker>(context), IWorkerRepository
{
    
}