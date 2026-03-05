using Blocks.Core.Cache;
using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
{

    public byte MaxFileSizeInMb { get; set; }
    public int MaxFileSizeInBytes => MaxFileSizeInMb * 1024 * 1024;
    public string DefaultFileExtension { get; set; } = default!;
    public required FileExtensions AllowedFileExtensions { get; set; }

    public int MaxAssetCount { get; init; }

    public bool AllowsMultipleAssets => MaxAssetCount > 1;
}
