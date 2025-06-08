

using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Repositories.PoliticalOrganisations;

public class MovementRepository(AppDbContext context) 
    : Repository<Movement>(context), IMovementRepository
{
    


    public Task AddMovementMemberAsync(int movementId, int politicianId, bool isLeader)
    {
        throw new NotImplementedException();
    }

    public Task EndMovementMembershipAsync(int membershipId)
    {
        throw new NotImplementedException();
    }

    public Task AddSupportingOrganisationAsync(int movementId, int supportingOrgId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveSupportingOrganisationAsync(int movementId, int supportingOrgId)
    {
        throw new NotImplementedException();
    }
}