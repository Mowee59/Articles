using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public class Person : Entity
{

    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public string FullName => $"{FirstName} {LastName}";
    public string? Title { get; set; }
    public required EmailAdress EmailAdress { get; set; }
    public required string Afiliation { get; set; }
    public int? UserId { get; init; } // Optional link to a user account, if applicable

    // Navigation property for the articles this person is involved in,
    // set to private set to prevent external modification, ensuring that the relationship is managed through the ArticleActor entity
    public IReadOnlyCollection<ArticleActor> ArticleActors { get; private set; } = new List<ArticleActor>();

    public string TypeDiscriminator { get; init; } = null!; // Discriminator for EF Core to distinguish between Author and Reviewer and other eventual inherited types of Person

}
