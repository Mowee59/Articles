using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;


namespace Submission.Persistence.EntityConfigurations;

internal class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(64).IsRequired();
        builder.Property(e => e.Abreviation).HasMaxLength(8).IsRequired();
    }
}
