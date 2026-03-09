using Blocks.Core.Cache;
using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFramework;

/// <summary>
/// Base repository that loads and caches all entities of a given type in memory.
/// Uses <see cref="IMemoryCache"/> with a type-based cache key.
/// </summary>
/// <typeparam name="TDbContext">EF Core DbContext type.</typeparam>
/// <typeparam name="TEntity">Entity type to cache.</typeparam>
/// <typeparam name="TId">Value-type primary key of the entity.</typeparam>
public abstract class CachedRepository<TDbContext, TEntity, TId>(TDbContext _dbContext, IMemoryCache _cache)
    where TDbContext : DbContext
    where TEntity : class, IEntity<TId>, ICacheable
    where TId : struct
{
    /// <summary>
    /// Returns all entities from the cache, querying the database and populating the cache on first access.
    /// </summary>
    /// <returns>All entities of type <typeparamref name="TEntity"/>.</returns>
    public IEnumerable<TEntity> GetAll()
        => _cache.GetOrCreateByType(entry => _dbContext.Set<TEntity>().AsNoTracking().ToList())!;

    /// <summary>
    /// Returns a single entity by its identifier from the cached collection.
    /// Throws if no entity or multiple entities match the provided id.
    /// </summary>
    /// <param name="id">Identifier of the entity to retrieve.</param>
    /// <returns>The matching entity.</returns>
    public TEntity GetById(TId id)
        => GetAll().Single(e => e.Id.Equals(id));
}
