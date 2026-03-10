
using Auth.Domain.Users.Enums;
using Auth.Domain.Users.ValueObjects;
using Blocks.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Users;

/// <summary>
/// Represents an application user within the authentication system.
/// Inherits from <see cref="IdentityUser{TKey}"/> with integer keys for ASP.NET Core Identity integration.
/// Implements <see cref="IEntity"/> for cross-layer consistency.
/// Stores basic identity information, professional profile, honorific title, and organizational roles.
/// </summary>
public partial class User : IdentityUser<int>, IEntity
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// The full name of the user, computed from first and last name.
    /// </summary>
    public string FullName => FirstName + " " + LastName;

    /// <summary>
    /// The user's gender value.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// The user's honorific title (e.g. Mr, Dr, Prof), if any.
    /// </summary>
    public HonorificTitle? Honorific { get; set; }

    /// <summary>
    /// The user's professional profile details (job, company, affiliation), if any.
    /// </summary>
    public ProfessionalProfile? ProfessionalProfile { get; set; }

    /// <summary>
    /// Optional URL to the user's profile picture.
    /// </summary>
    public string? PictureUrl { get; set; } = null;

    /// <summary>
    /// The UTC date and time the user registered.
    /// </summary>
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The UTC date and time of the user's most recent login.
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Backing field for user's assigned roles in the system.
    /// </summary>
    private List<UserRole> _userRoles = new List<UserRole>();

    /// <summary>
    /// Roles currently assigned to this user (read-only).
    /// </summary>
    public virtual IReadOnlyList<UserRole> UserRoles => _userRoles;
}
