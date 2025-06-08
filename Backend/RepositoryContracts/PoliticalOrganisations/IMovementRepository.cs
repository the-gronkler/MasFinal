using MasFinal.Models.PoliticalOrganisation;

namespace MasFinal.RepositoryContracts.PoliticalOrganisations;

public interface IMovementRepository : IRepository<Movement>
{
    Task AddMovementMemberAsync(int movementId, int politicianId, bool isLeader);
    Task EndMovementMembershipAsync(int membershipId);
    
    Task AddSupportingOrganisationAsync(int movementId, int supportingOrgId);
    Task RemoveSupportingOrganisationAsync(int movementId, int supportingOrgId);
}