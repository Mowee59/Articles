using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Aggregate root representing an article submitted to a journal, including its
/// metadata, stage, actors (authors), and associated assets.
/// </summary>
public partial class Article : Entity
{
    /// <summary>
    /// Title of the article.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Scope or abstract describing the article content.
    /// </summary>
    public required string Scope { get; set; }

    /// <summary>
    /// Type of article (e.g. research, review).
    /// </summary>
    public required ArticleType Type { get; set; }

    /// <summary>
    /// Current lifecycle stage of the article.
    /// </summary>
    public ArticleStage Stage { get; internal set; }

    /// <summary>
    /// Identifier of the journal this article belongs to.
    /// </summary>
    public int JournalId { get; init; }

    /// <summary>
    /// Journal aggregate to which this article is submitted.
    /// </summary>
    public required Journal Journal { get; init; }

    /// <summary>
    /// Collection of actors (e.g. authors) associated with this article.
    /// </summary>
    public List<ArticleActor> Actors { get; init; } = new();

    /// <summary>
    /// Backing list for article assets (e.g. manuscript files, supplementary materials).
    /// </summary>
    private readonly List<Asset> _assets = new();

    /// <summary>
    /// Read-only view of assets associated with this article.
    /// </summary>
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();
}
