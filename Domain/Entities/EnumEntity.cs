namespace Blocks.Domain.Entities;

/// <summary>
/// Base class for entities whose primary key is an enum value.
/// </summary>
/// <typeparam name="TEnum">Enum type used as the identifier.</typeparam>
public abstract class EnumEntity<TEnum> : Entity<TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Enum value associated with this entity.
    /// </summary>
    public TEnum Name { get; init; } = default!;
}
