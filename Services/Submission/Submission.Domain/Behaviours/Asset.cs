using FileStorage.Contracts;

namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset() { }


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

    public string GenerateStorageFilePath(string filename)
        => $"Articles/{Article.Id}/{Name}/{filename}";

    public File CreateFile(UploadResponse uploadRespone, AssetTypeDefinition assetType)
    {
        File = File.CreateFile(uploadRespone, this, assetType);

        return File;
    }
}
