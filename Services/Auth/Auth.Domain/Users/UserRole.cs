using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Users;

/// <summary>
/// Represents a user-to-role assignment within the authentication system.
/// Inherits from <see cref="IdentityUserRole{TKey}"/> for ASP.NET Core Identity integration.
/// Used to associate a user with a specific role, and optionally track assignment period details.
/// </summary>
public class UserRole : IdentityUserRole<int>
{
    /// <summary>
    /// The date when the user was assigned to this role.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The date when the role assignment expires (if any), represented as a string for flexibility.
    /// </summary>
    public string? ExpiringDate { get; set; }
}
