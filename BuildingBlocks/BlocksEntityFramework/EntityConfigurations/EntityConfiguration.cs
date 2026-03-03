using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Domain.Entities;

namespace Blocks.EntityFramework.EntityConfigurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnOrder(0);

    }
}
