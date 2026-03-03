using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public partial class Journal : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Abreviation { get; set; }

    private readonly List<Article> _articles = new();
    public IReadOnlyList<Article> Articles => _articles.AsReadOnly();
}
