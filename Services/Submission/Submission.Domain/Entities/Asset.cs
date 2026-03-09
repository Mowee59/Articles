using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Entity representing a logical asset attached to an article (e.g. manuscript, figure).
/// Owns the associated file metadata and links back to the article.
/// </summary>
public partial class Asset : Entity
{
    /// <summary>
    /// Logical name of the asset derived from its type and owning article.
    /// </summary>
    public AssetName Name { get; private set; } = null!;
    /// <summary>
    /// Kind of asset (e.g. manuscript, supplementary file).
    /// </summary>
    public AssetType Type { get; private set; }
    /// <summary>
    /// Identifier of the article that owns this asset.
    /// </summary>
    public int ArticleId { get; private set; }
    /// <summary>
    /// Article that owns this asset.
    /// </summary>
    public virtual Article Article { get; private set; } = null!;
    /// <summary>
    /// File metadata associated with this asset in storage.
    /// </summary>
    public File File { get; set; } = null!;

}
