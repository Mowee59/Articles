using Blocks.EntityFramework;
using FileStorage.Contracts;
using Submission.Persistence;

namespace Submission.Application.Features.UploadFile;

/// <summary>
/// Handles <see cref="UploadManuscriptFileCommand"/> requests by creating or updating
/// an article asset and delegating the physical file upload to the file storage service.
/// </summary>
public class UploadManuscriptFileCommandHandler(ArticleRepository _articleRepository,
                                                AssetTypeDefinitionRepository _assetTypeRepository,
                                                IFileService _fileService
                                                )
    : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    /// <summary>
    /// Resolves the article and asset type, ensures the correct asset exists (respecting
    /// single/multiple asset rules), uploads the file via <see cref="IFileService"/>,
    /// links the uploaded file to the asset, saves changes, and returns the asset id.
    /// If persistence fails after upload, the file is deleted from storage.
    /// </summary>
    /// <param name="command">The upload-manuscript command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An <see cref="IdResponse"/> containing the asset id.</returns>
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var assetType = _assetTypeRepository.GetById(command.AssetType);

        Asset asset = null;

        if (!assetType.AllowsMultipleAssets)
            asset = article.Assets.SingleOrDefault(e => e.Type == assetType.Id);

        if (asset is null)
            asset = article.CreateAsset(assetType);

        var filePath =  asset.GenerateStorageFilePath(command.File.FileName);
        var uploadResponse = await _fileService.UploadFileAsync(
            filePath, 
            command.File, 
            overwrite: true, 
            tags: new Dictionary<string, string>
        {
            {"entity", nameof(Asset)},
            {"entityId", asset.Id.ToString() }
        });

        try
        {
            asset.CreateFile(uploadResponse, assetType);

            await _articleRepository.SaveChangesAsync();
        }
        catch(Exception)
        {
            await _fileService.TryDeleteFileAsync(uploadResponse.FileId);
            throw;
        }
        

        return new IdResponse(asset.Id);

    }
}
