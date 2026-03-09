using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Join entity linking a <see cref="Person"/> to an <see cref="Article"/> with a specific role.
/// Base type for more specialized actor types such as <see cref="ArticleAuthor"/>.
/// </summary>
public class ArticleActor : Entity
{
    /// <summary>
    /// Identifier of the associated article.
    /// </summary>
    public int ArticleId { get; init; }
    /// <summary>
    /// Article this actor participates in.
    /// </summary>
    public Article Article { get; init; } = null!;
    /// <summary>
    /// Identifier of the participating person.
    /// </summary>
    public int PersonId { get; init; }
    /// <summary>
    /// Person participating in the article (e.g. author).
    /// </summary>
    public Person Person { get; init; } = null!;
    /// <summary>
    /// Role of the person on the article (e.g. author, corresponding author).
    /// </summary>
    public UserRoleType Role { get; init; }
    /// <summary>
    /// EF Core discriminator to distinguish between concrete article actor types.
    /// </summary>
    public string TypeDiscriminator { get; init; } = null!;
}
