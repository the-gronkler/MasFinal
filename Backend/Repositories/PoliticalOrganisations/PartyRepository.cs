using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Repositories.PoliticalOrganisations;

public class PartyRepository (AppDbContext context) 
    : Repository<Party>(context), IPartyRepository
{

    public Task AddPartyMemberAsync(int partyId, int politicianId, PartyPosition position)
    {
        throw new NotImplementedException();
    }

    public Task EndPartyMembershipAsync(int membershipId)
    {
        throw new NotImplementedException();
    }
}