using MasFinal.Data;
using MasFinal.Models.Businesses;
using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;

namespace MasFinal.Repositories.Businesses;

public class BusinessRepository(AppDbContext context) 
    : Repository<Business>(context), IBusinessRepository;
    
    