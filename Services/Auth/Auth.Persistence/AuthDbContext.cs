using Auth.Domain.Users;
using Auth.Domain.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence;

/// <summary>
/// Entity Framework Core database context for the authentication service.
/// Inherits from <see cref="IdentityDbContext{TUser, TRole, TKey}"/> to provide ASP.NET Core Identity integration
/// with integer keys for users and roles.
/// Responsible for configuring authentication-related entities (users, roles, user roles) and applying
/// entity configurations declared within the persistence layer's assembly.
/// </summary>
public class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : IdentityDbContext<User, Role, int>(options)
{
    /// <summary>
    /// Configures the database schema for authentication entities and applies all entity configurations
    /// found in the current assembly. This includes mappings for users, roles, user role assignments,
    /// and any owned types or value objects.
    /// </summary>
    /// <param name="builder">The builder used to configure entity models for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Invoke base configuration (handles ASP.NET Identity tables, relationships, etc.)
        base.OnModelCreating(builder);

        // Apply all IEntityTypeConfiguration implementations within this assembly
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}