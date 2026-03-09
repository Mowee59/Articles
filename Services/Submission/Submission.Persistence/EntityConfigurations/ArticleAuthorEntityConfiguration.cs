using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// EF Core configuration for <see cref="ArticleAuthor"/> entities, mapping contribution
/// areas as a JSON collection.
/// </summary>
internal class ArticleAuthorEntityConfiguration : IEntityTypeConfiguration<ArticleAuthor>
{


    /// <summary>
    /// Configures the JSON conversion for the <c>ContributionAreas</c> collection.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<ArticleAuthor> builder)
    {
        builder.Property(e => e.ContributionAreas)
                   .HasJsonCollectionConversion()
                   .IsRequired();
    }
}
