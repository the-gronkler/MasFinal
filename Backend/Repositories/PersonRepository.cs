using MasFinal.Models;
using MasFinal.RepositoryContracts;

namespace MasFinal.Repositories;

public class PersonRepository(AppDbContext context) 
    : Repository<Person>(context), IPersonRepository
{
    public Task<Person?> GetPersonWithDetailsAsync(int personId)
    {
        throw new NotImplementedException();
    }

    public Task BecomePoliticianAsync(int personId, int influenceScore)
    {
        throw new NotImplementedException();
    }

    public Task BecomeOligarchAsync(int personId, double wealth)
    {
        throw new NotImplementedException();
    }

    public Task QuitAsPoliticianAsync(int personId)
    {
        throw new NotImplementedException();
    }

    public Task QuitAsOligarchAsync(int personId)
    {
        throw new NotImplementedException();
    }
}