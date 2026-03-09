using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// EF Core configuration for <see cref="Asset"/> entities, including enum conversion,
/// owned name value object, and nested file configuration.
/// </summary>
internal class AssetEntityConfiguration : EntityConfiguration<Asset>
{
    /// <summary>
    /// Configures the asset mapping, setting up enum storage for <c>Type</c>,
    /// complex property mapping for <c>Name</c>, and applying <see cref="FileEntityConfiguration"/>
    /// for the owned <c>File</c> value object.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Type).HasEnumConversion();

        builder.ComplexProperty(
            o => o.Name, builder =>
            {
                builder.Property(n => n.Value)
                    .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64)
                    .IsRequired();

            });

        builder.ComplexProperty(
            e => e.File, fileBuilder =>
            {
              new FileEntityConfiguration().Configure(fileBuilder);
            });

    }
}
