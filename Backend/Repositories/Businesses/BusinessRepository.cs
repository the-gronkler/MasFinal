using MasFinal.Data;
using MasFinal.Models.Businesses;
using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;

namespace MasFinal.Repositories.Businesses;

public class BusinessRepository(AppDbContext context)
    : Repository<Business>(context), IBusinessRepository
{

    public override void Update(Business entity)
    {
        var originalEntity = _context.Entry(entity).OriginalValues;
        var currentEntity = _context.Entry(entity).CurrentValues;

        if (originalEntity.GetValue<int>(nameof(Business.OwnerId))
            != currentEntity.GetValue<int>(nameof(Business.OwnerId)))
            throw new InvalidOperationException(
                "A Business cannot be moved to a different Owner. The ownership relationship is permanent.");

        base.Update(entity);
    }

}
    