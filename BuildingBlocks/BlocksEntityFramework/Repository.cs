using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;


/// <summary>
/// Basic repository contract for entities with an integer primary key.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Finds an entity by id using the underlying EF tracking behavior.
    /// Returns <c>null</c> if not found.
    /// </summary>
    Task<TEntity?> FindByIdAsync(int id);

    /// <summary>
    /// Queries the data source for an entity with the given id.
    /// Returns <c>null</c> if not found.
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new entity to the context.
    /// </summary>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Marks an entity as modified in the context.
    /// </summary>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Removes an entity from the context.
    /// </summary>
    void Remove(TEntity entity);

    /// <summary>
    /// Deletes an entity by id using a direct SQL DELETE statement.
    /// Returns <c>true</c> if at least one row was affected.
    /// </summary>
    Task<bool> DeleteByIdAsync(int id);

    /// <summary>
    /// Persists pending changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken ct);

}

/// <summary>
/// Default EF Core-based implementation of <see cref="IRepository{TEntity}"/>.
/// Provides basic CRUD operations for a given <typeparamref name="TEntity"/> in a <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">EF Core DbContext type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{

    protected readonly TContext _dbContext;
    protected readonly DbSet<TEntity> _entity;

    /// <summary>
    /// Database table name mapped to <typeparamref name="TEntity"/>.
    /// </summary>
    public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

    /// <summary>
    /// Initializes a new repository for the given DbContext.
    /// </summary>
    public Repository(TContext dbContext)
    {
        _dbContext = dbContext;
        _entity = _dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Underlying DbContext instance.
    /// </summary>
    public TContext Context => _dbContext;

    /// <summary>
    /// Underlying DbSet for the entity type.
    /// </summary>
    public virtual DbSet<TEntity> DbSet => _entity;

    /// <summary>
    /// Base query for the entity; can be overridden to include filters or related data.
    /// </summary>
    protected virtual IQueryable<TEntity> Query() => _entity;

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity)
        => await _entity.AddAsync(entity);

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(int id)
    {
        var rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(int id) 
        => await Query().FirstOrDefaultAsync(e => e.Id.Equals(id));

    /// <inheritdoc />
    public void Remove(TEntity entity)
        => _entity.Remove(entity);

    /// <inheritdoc />
    public virtual TEntity Update(TEntity entity)
     => _entity.Update(entity).Entity;

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync();

    /// <inheritdoc />
    public async Task<TEntity?> FindByIdAsync(int id)
        => await _entity.FindAsync(id);
}
