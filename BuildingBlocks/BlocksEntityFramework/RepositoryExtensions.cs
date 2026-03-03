

using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Blocks.EntityFramework;

/// <summary>
/// Provides utility extension methods for Entity Framework repositories.
/// </summary>
public static class RepositoryExtensions
{
  

    /// <summary>
    /// Retrieves an entity by its identifier or throws an exception if it is not found.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity handled by the repository.</typeparam>
    /// <typeparam name="TContext">The type of the Entity Framework context used by the repository.</typeparam>
    /// <param name="repository">The repository instance on which this extension method is called.</param>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>The matching entity if it exists.</returns>
    /// <exception cref="HttpRequestException">Thrown when no entity matches the provided identifier.</exception>
    public static  async Task<TEntity> FindByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        var entity = await repository.FindByIdAsync(id);
        if(entity is null)
        {
            throw new HttpRequestException($"{typeof(TEntity).Name} not found.");
        }

        return entity;
    }

}
