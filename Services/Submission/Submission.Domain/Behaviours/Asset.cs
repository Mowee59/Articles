using FileStorage.Contracts;

namespace Submission.Domain.Entities;

public partial class Asset
{
    /// <summary>
    /// Use <see cref="Create(Article, AssetTypeDefinition)"/> instead.
    /// </summary>
    private Asset() { }


    /// <summary>
    /// Factory method to create a new asset for the given article and asset type.
    /// Sets up basic relationships and default name based on the type.
    /// </summary>
    /// <param name="article">Article that owns the asset.</param>
    /// <param name="type">Asset type definition.</param>
    /// <returns>A newly created asset instance.</returns>
    internal static Asset Create(Article article, AssetTypeDefinition type)
    {
        return new Asset()
        {
            ArticleId = article.Id,
            Article = article,
            Name = AssetName.FromAssetType(type),
            Type = type.Name
        };
    }

    /// <summary>
    /// Generates the storage path for a physical file belonging to this asset.
    /// </summary>
    /// <param name="filename">Original file name.</param>
    /// <returns>Relative storage path used by the file storage provider.</returns>
    public string GenerateStorageFilePath(string filename)
        => $"Articles/{Article.Id}/{Name}/{filename}";

    /// <summary>
    /// Creates or updates the logical file entity for this asset from an upload response.
    /// </summary>
    /// <param name="uploadRespone">Result of the file upload operation.</param>
    /// <param name="assetType">Asset type used to configure file behavior.</param>
    /// <returns>The associated <see cref="File"/> entity.</returns>
    public File CreateFile(UploadResponse uploadRespone, AssetTypeDefinition assetType)
    {
        File = File.CreateFile(uploadRespone, this, assetType);

        return File;
    }
}
