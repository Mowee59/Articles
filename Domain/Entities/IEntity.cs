namespace Blocks.Domain.Entities;

public interface IEntity : IEntity<int>
{
}

public abstract class Entity : IEntity
{
    public virtual int Id { get; init; }
}

public interface IEntity<TPrimaryKey>
{
    public TPrimaryKey Id { get; }
}


public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    public virtual TPrimaryKey Id { get; init; }
}