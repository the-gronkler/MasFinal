using MasFinal.Models.PoliticalOrganisation;

namespace MasFinal.RepositoryContracts.PoliticalOrganisations;

public interface IPartyRepository : IRepository<Party>
{
    Task AddPartyMemberAsync(int partyId, int politicianId, PartyPosition position);
    Task EndPartyMembershipAsync(int membershipId);
}