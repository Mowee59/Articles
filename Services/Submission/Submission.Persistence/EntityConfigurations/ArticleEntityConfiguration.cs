using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// EF Core configuration for <see cref="Article"/> entities, including property
/// constraints and relationships to journals and assets.
/// </summary>
internal class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    /// <summary>
    /// Configures title and scope lengths, enum storage for stage and type,
    /// and relationships to <see cref="Journal"/> and <see cref="Asset"/> entities.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title).HasMaxLength(256).IsRequired();

        builder.Property(e => e.Scope).HasMaxLength(2048).IsRequired();

        builder.Property(e => e.Stage).HasEnumConversion();

        builder.Property(e => e.Type).HasEnumConversion();

        builder.HasOne(e => e.Journal)
            .WithMany(e => e.Articles)
            .HasForeignKey(e => e.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Assets)
            .WithOne(e => e.Article)
            .HasForeignKey(e => e.ArticleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
