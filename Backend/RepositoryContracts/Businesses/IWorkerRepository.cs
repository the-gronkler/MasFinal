using MasFinal.Models.Businesses;

namespace MasFinal.RepositoryContracts.Businesses;

public interface IWorkerRepository : IRepository<Worker>
{
     Task<double> GetMinimumWageAsync();
     Task UpdateMinimumWageAsync(double newValue);
}