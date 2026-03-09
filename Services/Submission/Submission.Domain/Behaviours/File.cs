using FileStorage.Contracts;

namespace Submission.Domain.ValueObjects;

public partial class File
{
    /// <summary>
    /// Use <see cref="CreateFile(UploadResponse, Asset, AssetTypeDefinition)"/> instead.
    /// </summary>
    private File(){}

    /// <summary>
    /// Factory method to create a new file value object from an upload response,
    /// associating it with an asset and deriving name and extension.
    /// </summary>
    /// <param name="uploadResponse">Result of the file upload operation.</param>
    /// <param name="asset">Asset that owns this file.</param>
    /// <param name="assetType">Asset type used to validate and derive extension.</param>
    /// <returns>A fully initialized <see cref="File"/> value object.</returns>
    public static File CreateFile(UploadResponse uploadResponse, Asset asset, AssetTypeDefinition assetType)
    {
        var fileName = Path.GetFileName(uploadResponse.FilePath);
        var extension = FileExtension.FromFileName(fileName, assetType);
        var file = new File()
        {
            Name = FileName.FromAsset(asset, extension),
            Extension = extension,
            OriginalName = fileName,
            Size = uploadResponse.FileSize,
            FileServerId = uploadResponse.FileId

        };

        return file;
    }
}
