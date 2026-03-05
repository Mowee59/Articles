using Blocks.Core.FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    /// <summary>
    /// The asset type fo the file
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }

    /// <summary>
    /// The file to be uploaded
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

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