using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Repositories.PoliticalOrganisations;

public class PoliticalOrganisationRepository(AppDbContext context) 
    : Repository<PoliticalOrganisation>(context), IPoliticalOrganisationRepository
{
    public Task<bool> IsNameUniqueAsync(string name, int? orgId = null)
    {
        throw new NotImplementedException();
    }
}