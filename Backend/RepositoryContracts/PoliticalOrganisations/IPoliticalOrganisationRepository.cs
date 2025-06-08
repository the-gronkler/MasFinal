using MasFinal.Models.PoliticalOrganisation;

namespace MasFinal.RepositoryContracts.PoliticalOrganisations;

public interface IPoliticalOrganisationRepository
{
    Task<bool> IsNameUniqueAsync(string name, int? orgId = null);
}

