namespace Blocks.Domain.Entities;

/// <summary>
/// Entity with an <see cref="int"/> primary key (default case).
/// </summary>
public interface IEntity : IEntity<int>
{
}

/// <summary>
/// Base class for entities with an <see cref="int"/> key.
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// Database identity, set once by the persistence layer.
    /// </summary>
    public virtual int Id { get; init; }
}

/// <summary>
/// Contract for entities with a strongly typed primary key.
/// </summary>
/// <typeparam name="TPrimaryKey">Primary key type (e.g. <see cref="int"/>, <see cref="Guid"/>).</typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// Strongly typed primary key.
    /// </summary>
    public TPrimaryKey Id { get; }
}

/// <summary>
/// Base class for entities with a value-type primary key.
/// </summary>
/// <typeparam name="TPrimaryKey">Value type used as the primary key.</typeparam>
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    /// <summary>
    /// Primary key, init-only to keep identity immutable.
    /// </summary>
    public virtual TPrimaryKey Id { get; init; }
}