using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// EF Core configuration for the <see cref="Person"/> hierarchy, including discriminator,
/// indexes, and value object mappings.
/// </summary>
internal class PersonEntityConfiguration : EntityConfiguration<Person>
{
    /// <summary>
    /// Configures the table-per-hierarchy mapping for <see cref="Person"/> and its subtypes,
    /// as well as property lengths, optional user link, and the owned <c>EmailAdress</c> value object.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasDiscriminator(e => e.TypeDiscriminator)  // Discriminator column because we are using TPH (Table Per Hierarchy) inheritance strategy
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author));

        builder.Property(e => e.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(64).IsRequired();
        builder.Property(e => e.Title).HasMaxLength(64);
        builder.Property(e => e.Afiliation).HasMaxLength(512).IsRequired()
            .HasComment("Insitution or organization they are affiliated with when cunducting their research");
        builder.Property(e => e.UserId).IsRequired(false);

        builder.ComplexProperty(o => o.EmailAdress, builder =>
        {
            builder.Property(n => n.Value)
             .HasColumnName(builder.Metadata.PropertyInfo!.Name)
             .HasMaxLength(64);
        });
    }
}
