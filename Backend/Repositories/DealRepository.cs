using MasFinal.Models;
using MasFinal.RepositoryContracts;

namespace MasFinal.Repositories;

public class DealRepository(AppDbContext context) 
    : Repository<Deal>(context), IDealRepository
{
    
}