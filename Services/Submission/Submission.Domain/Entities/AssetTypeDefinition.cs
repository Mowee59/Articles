using Blocks.Core.Cache;
using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

/// <summary>
/// Configuration entity describing how a particular <see cref="AssetType"/> should be stored,
/// including file size limits, allowed extensions, and multiplicity rules.
/// </summary>
public class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
{
    /// <summary>
    /// Maximum allowed file size in megabytes.
    /// </summary>
    public byte MaxFileSizeInMb { get; set; }
    /// <summary>
    /// Maximum allowed file size in bytes, derived from <see cref="MaxFileSizeInMb"/>.
    /// </summary>
    public int MaxFileSizeInBytes => MaxFileSizeInMb * 1024 * 1024;
    /// <summary>
    /// Default file extension for this asset type.
    /// </summary>
    public string DefaultFileExtension { get; set; } = default!;
    /// <summary>
    /// Set of allowed file extensions for this asset type.
    /// </summary>
    public required FileExtensions AllowedFileExtensions { get; set; }

    /// <summary>
    /// Maximum number of assets of this type allowed per article.
    /// </summary>
    public int MaxAssetCount { get; init; }

    /// <summary>
    /// Indicates whether more than one asset of this type can be attached to an article.
    /// </summary>
    public bool AllowsMultipleAssets => MaxAssetCount > 1;
}
