using Articles.Abstractions.Enums;
using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class FileExtension : StringValueObject
{
    private FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetType)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1); // Removing the dot from the extension

        Guard.ThrowIfNullOrWhiteSpace(extension); 

        Guard.ThrowIfNotEqual(assetType.AllowedFileExtensions.IsValidExtension(extension), true); // Validating if the extension is allowed for the given asset type

        return new FileExtension(extension);
    }

}
