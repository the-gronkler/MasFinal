using System.Linq.Expressions;

namespace MasFinal.RepositoryContracts;

/// <summary>
/// Generic repository with common data access methods for an entity type.
/// </summary>
/// <typeparam name="T">The entity type this repository operates on. Must be a class.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves an entity by its primary key.
    /// </summary>\
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously retrieves ALL entities of type <typeparamref name="T"/> 
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Asynchronously finds all entities that satisfy a specified condition.
    /// </summary>
    /// <param name="predicate">An expression to test each entity for a condition.</param>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Adds a new entity to the context.
    /// </summary>
    /// <remarks>
    /// This method only marks the entity as 'Added' in the change tracker.
    /// The entity will not be persisted to the database until <see cref="SaveChangesAsync"/> is called.
    /// </remarks>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Marks an existing entity as modified in the data store's context.
    /// </summary>
    /// <remarks>
    /// This method attaches the entity to the change tracker and sets its state to 'Modified'.
    /// All properties of the entity will be marked for an update in the database.
    /// The changes will not be persisted until <see cref="SaveChangesAsync"/> is called.
    /// </remarks>
    void Update(T entity);

    /// <summary>
    /// Marks an existing entity for deletion from the data store.
    /// </summary>
    /// <remarks>
    /// This method sets the entity's state to 'Deleted' in the change tracker.
    /// The entity will not be removed from the database until <see cref="SaveChangesAsync"/> is called.
    /// </remarks>
    void Remove(T entity);

    /// <summary>
    /// Asynchronously saves all changes made in the context to the database.
    /// </summary>
    Task<int> SaveChangesAsync();
}