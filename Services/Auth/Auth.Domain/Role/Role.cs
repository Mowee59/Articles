using Articles.Abstractions.Enums;
using Microsoft.AspNetCore.Identity;
using Blocks.Domain.Entities;

namespace Auth.Domain.Role;

/// <summary>
/// Represents a user role within the authentication system.
/// Inherits from <see cref="IdentityRole{TKey}"/> for integration with ASP.NET Core Identity,
/// and implements <see cref="IEntity"/> for domain-driven design compliance.
/// </summary>
public class Role : IdentityRole<int>, IEntity
{
    /// <summary>
    /// Gets or sets the specific type of user role, corresponding to application-defined role categories.
    /// </summary>
    public required UserRoleType Type { get; set; }
    
    /// <summary>
    /// Gets or sets a human-readable description of the role for display and documentation purposes.
    /// </summary>
    public required string Desciption { get; set; }
}
