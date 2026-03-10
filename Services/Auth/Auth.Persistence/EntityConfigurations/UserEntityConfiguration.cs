using Auth.Domain.Users;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace Auth.Persistence.EntityConfigurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="User"/> entity.
/// Maps properties and value objects to database columns, applies constraints, and
/// defines relationships within the authentication system persistence layer.
/// </summary>
internal class UserEntityConfiguration : EntityConfiguration<User>
{
    /// <summary>
    /// Configures the <see cref="User"/> entity's persistence mapping, including
    /// owned value objects, property constraints, and navigation properties.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        // Invoke base configuration (e.g., Id, concurrency token)
        base.Configure(builder);

        // Configure basic property constraints
        builder.Property(e => e.FirstName)
            .HasMaxLength(MaxLength.C64)
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(MaxLength.C64)
            .IsRequired();

        // Gender is a required enum, store as converted value
        builder.Property(e => e.Gender)
            .HasEnumConversion()
            .IsRequired();

        // Map optional Honorific value object as an owned type (single table, nullable)
        builder.OwnsOne(
            e => e.Honorific, b =>
        {
            b.Property(n => n.Value)
                .HasColumnName(nameof(User.Honorific))
                .HasMaxLength(MaxLength.C32);

            // Ensures the value object is embedded in User table
            b.WithOwner();
        });

        // Map optional ProfessionalProfile value object as an owned type (single table, nullable)
        builder.OwnsOne(e => e.ProfessionalProfile, b =>
        {
            b.Property(n => n.Position)
                .HasColumnNameAsProperty()
                .HasMaxLength(MaxLength.C32);
            b.Property(n => n.CompanyName)
                .HasColumnNameAsProperty()
                .HasMaxLength(MaxLength.C32);
            b.Property(n => n.Affiliation)
                .HasColumnNameAsProperty()
                .HasMaxLength(MaxLength.C32);

            // Integrated within the main User table
            b.WithOwner();
        });

        // Optional profile picture URL, allow up to 2048 chars
        builder.Property(e => e.PictureUrl)
            .HasMaxLength(MaxLength.C2048);

        // Configure one-to-many relationship with UserRoles
        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Roles deleted when User is deleted
    }
}
