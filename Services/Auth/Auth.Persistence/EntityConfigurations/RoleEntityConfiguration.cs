using Auth.Domain.Roles;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;

namespace Auth.Persistence.EntityConfigurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Role"/> entity.
/// Configures property mappings, constraints, and conversions for roles within the authentication system.
/// </summary>
public class RoleEntityConfiguration : EntityConfiguration<Role>
{
    /// <summary>
    /// Configures the <see cref="Role"/> entity's property mappings and constraints for EF Core.
    /// Enforces enum conversion, maximum length restrictions, and required fields.
    /// </summary>
    /// <param name="builder">The EntityTypeBuilder for <see cref="Role"/>.</param>
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Type)
            .HasEnumConversion()
            .IsRequired();

        builder.Property(e => e.Desciption)
            .HasMaxLength(MaxLength.C256)
            .IsRequired();
    }
}