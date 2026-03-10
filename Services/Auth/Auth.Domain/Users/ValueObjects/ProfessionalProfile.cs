using Blocks.Domain.ValueObjects;

namespace Auth.Domain.Users.ValueObjects;

/// <summary>
/// Value object representing a user's professional profile, including their position, company, and affiliation.
/// Used within the authentication system to store and compare professional background details.
/// </summary>
public class ProfessionalProfile : ValueObject
{
    /// <summary>
    /// The user's job title or professional position.
    /// </summary>
    public string? Position { get; set; }
    
    /// <summary>
    /// The name of the company or organization the user is associated with.
    /// </summary>
    public string? CompanyName { get; set; }
    
    /// <summary>
    /// The user's additional affiliation (e.g., university, professional body).
    /// </summary>
    public string? Affiliation { get; set; }

    /// <summary>
    /// Private constructor for EF Core. Use the factory method <see cref="Create"/>.
    /// </summary>
    private ProfessionalProfile() { }

    /// <summary>
    /// Factory method to create a <see cref="ProfessionalProfile"/> instance.
    /// Trims whitespace and normalizes empty input to <c>null</c>.
    /// </summary>
    /// <param name="position">Job title or position.</param>
    /// <param name="companyName">Company or organization name.</param>
    /// <param name="affiliation">Additional affiliation (optional).</param>
    /// <returns>A normalized <see cref="ProfessionalProfile"/> value object.</returns>
    public static ProfessionalProfile Create(string? position, string? companyName, string? affiliation)
    {
        return new ProfessionalProfile()
        {
            Position = string.IsNullOrWhiteSpace(position) ? null : position.Trim(),
            CompanyName = string.IsNullOrWhiteSpace(companyName) ? null : companyName.Trim(),
            Affiliation = string.IsNullOrWhiteSpace(affiliation) ? null : affiliation.Trim()
        };
    }

    /// <summary>
    /// Returns the atomic values for equality comparisons.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Position;
        yield return CompanyName;
        yield return Affiliation;
    }

    /// <summary>
    /// Returns a string representation of the professional profile.
    /// Format: "Position @ CompanyName", omitting empty elements.
    /// </summary>
    public override string ToString()
        => $"{Position}{(string.IsNullOrEmpty(Position) || string.IsNullOrEmpty(CompanyName) ? "" : " @ ")}{CompanyName}".Trim();

}
