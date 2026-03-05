using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public class Asset : Entity
{

    public AssetName Name { get; private set; } = null!;
    public AssetType AsstetType { get; private set; }
    public int ArticleId { get; private set; }
    public virtual Article Article { get; private set; } = null!;
    public File File { get; set; } = null!;

}
