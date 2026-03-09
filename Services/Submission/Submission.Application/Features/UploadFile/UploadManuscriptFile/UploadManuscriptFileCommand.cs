using Blocks.Core.FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile;

/// <summary>
/// Command to upload a manuscript file asset for an article.
/// </summary>
public record UploadManuscriptFileCommand : ArticleCommand
{
    /// <summary>
    /// Asset type for the uploaded file.
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }

    /// <summary>
    /// File to be uploaded as the manuscript.
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

    /// <summary>
    /// Action type representing a file upload.
    /// </summary>
    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

/// <summary>
/// Validator for <see cref="UploadManuscriptFileCommand"/> enforcing allowed asset type
/// and presence of a file to upload.
/// </summary>
public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(r => r.AssetType)
            .Must(isAllowedAssetType)
            .WithMessage(x => $"{ x.AssetType } not allowed");

        // TODO - validate file size and extension

        RuleFor(x => x.File)
            .NotNullWithMessage();
    }

    public IReadOnlyCollection<AssetType> AllowedAsseTypes = new HashSet<AssetType> { AssetType.Manuscript };

    private bool isAllowedAssetType(AssetType assetType)
    {
        return AllowedAsseTypes.Contains(assetType);
    }

}