using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;


public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> FindByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);

    Task<bool> DeleteByIdAsync(int id);

    Task<int> SaveChangesAsync(CancellationToken ct);

}

public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{

    protected readonly TContext _dbContext;
    protected readonly DbSet<TEntity> _entity;
    public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

    public Repository(TContext dbContext)
    {
        _dbContext = dbContext;
        _entity = _dbContext.Set<TEntity>();
    }

    public TContext Context => _dbContext;
    public virtual DbSet<TEntity> DbSet => _entity;

    protected virtual IQueryable<TEntity> Query() => _entity;

    public async Task AddAsync(TEntity entity)
        => await _entity.AddAsync(entity);

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
        return rowsAffected > 0;
    }

    public async Task<TEntity?> GetByIdAsync(int id) 
        => await Query().FirstOrDefaultAsync(e => e.Id.Equals(id));
    public void Remove(TEntity entity)
        => _entity.Remove(entity);

    public virtual TEntity Update(TEntity entity)
     => _entity.Update(entity).Entity;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync();

    public async Task<TEntity?> FindByIdAsync(int id)
        => await _entity.FindAsync(id);
}
