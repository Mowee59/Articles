using Articles.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;
using Blocks.EntityFramework;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArticleActor> builder)
    {
        builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role }); // Composite primary key

        builder.HasDiscriminator(e => e.TypeDiscriminator) // Discriminator column for TPH inheritance
            .HasValue<ArticleActor>(nameof(ArticleActor))
            .HasValue<Author>(nameof(ArticleAuthor));

        builder.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT); // Assuming 0 is the default role

        builder.HasOne(e => e.Article)
            .WithMany(a => a.Actors)
            .HasForeignKey(e => e.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Person)
            .WithMany(p => p.ArticleActors)
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
