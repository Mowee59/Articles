namespace Submission.Domain.Entities;

/// <summary>
/// Specialized article actor representing an author on an article, including
/// their specific contribution areas.
/// </summary>
public class ArticleAuthor : ArticleActor
{
    /// <summary>
    /// Contribution areas for this author on the associated article.
    /// </summary>
    public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}