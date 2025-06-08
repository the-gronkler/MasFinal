using MasFinal.Models;

namespace MasFinal.RepositoryContracts;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> GetPersonWithDetailsAsync(int personId);
    Task BecomePoliticianAsync(int personId, int influenceScore);
    Task BecomeOligarchAsync(int personId, double wealth);
    Task QuitAsPoliticianAsync(int personId);
    Task QuitAsOligarchAsync(int personId);
}