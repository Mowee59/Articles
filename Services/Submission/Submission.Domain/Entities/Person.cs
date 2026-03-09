using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Base entity representing a person involved in submissions (e.g. author, reviewer).
/// Holds common identity, contact, and affiliation information.
/// </summary>
public class Person : Entity
{
    /// <summary>
    /// Person's first name.
    /// </summary>
    public required string FirstName { get; init; }
    /// <summary>
    /// Person's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Convenience full name composed of first and last name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
    /// <summary>
    /// Optional title or honorific (e.g. Dr., Prof.).
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Email address of the person.
    /// </summary>
    public required EmailAdress EmailAdress { get; set; }
    /// <summary>
    /// Institution or organization the person is affiliated with.
    /// </summary>
    public required string Afiliation { get; set; }
    /// <summary>
    /// Optional link to a user account in the user service, when applicable.
    /// </summary>
    public int? UserId { get; init; }

    /// <summary>
    /// Articles this person is involved in, managed via <see cref="ArticleActor"/> relations.
    /// </summary>
    public IReadOnlyCollection<ArticleActor> ArticleActors { get; private set; } = new List<ArticleActor>();

    /// <summary>
    /// EF Core discriminator to distinguish between concrete person types (e.g. Author, Reviewer).
    /// </summary>
    public string TypeDiscriminator { get; init; } = null!;

}
