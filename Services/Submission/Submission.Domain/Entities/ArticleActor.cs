using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

public class ArticleActor
{
    public int ArticleId { get; init; }
    public Article Article { get; init; } = null!;
    public int PersonId { get; init; }
    public Person Person { get; init; } = null!;
    public UserRoleType Role { get; init; } // TODO - Consider using an enum for roles (e.g., Author, Reviewer, Editor) for better readability and maintainability

    public string TypeDiscriminator { get; init; } = null!; // Discriminator for EF Core to distinguish between  inherited types of ArticleActor
}
