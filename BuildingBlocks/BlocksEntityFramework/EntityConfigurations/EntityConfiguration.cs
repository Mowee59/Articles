using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Domain.Entities;

namespace Blocks.EntityFramework.EntityConfigurations;

/// <summary>
/// Base EF Core configuration for entities with an integer primary key.
/// Sets the <see cref="IEntity.Id"/> as the key and configures it as value-generated on add.
/// </summary>
/// <typeparam name="TEntity">Entity type being configured.</typeparam>
public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class, IEntity
{
    /// <summary>
    /// Configures the primary key and key generation strategy for the entity.
    /// Derived configurations can extend this method for additional mappings.
    /// </summary>
    /// <param name="builder">The EF Core entity type builder.</param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnOrder(0);

    }
}
