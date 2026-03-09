using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Aggregate root representing a journal that receives article submissions.
/// Holds journal metadata and the collection of submitted articles.
/// </summary>
public partial class Journal : Entity
{
    /// <summary>
    /// Full name of the journal.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Short abbreviation used to reference the journal.
    /// </summary>
    public required string Abreviation { get; set; }

    /// <summary>
    /// Backing list for articles submitted to this journal.
    /// </summary>
    private readonly List<Article> _articles = new();
    /// <summary>
    /// Read-only view of all articles submitted to the journal.
    /// </summary>
    public IReadOnlyList<Article> Articles => _articles.AsReadOnly();
}
