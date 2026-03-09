using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

/// <summary>
/// MongoDB GridFS implementation of <see cref="IFileService"/>.
/// Handles file upload, download, and deletion with metadata support.
/// </summary>
public class FileService : IFileService
{

    private readonly GridFSBucket _bucket;
    private readonly MongoGridFsFileStorageOptions _options;

    /// <summary>
    /// Metadata key used to store the logical file path in GridFS.
    /// </summary>
    private const string FilePathMetadataKey = "filepath";

    /// <summary>
    /// Metadata key used to store the content type in GridFS.
    /// </summary>
    private const string ContentTypeMetadataKey = "contentType";

    /// <summary>
    /// Fallback content type when none is stored with the file.
    /// </summary>
    public const string DefaultContentType = "application/octet-stream";

    /// <summary>
    /// Uploads a file to GridFS with metadata and size validation.
    /// </summary>
    /// <param name="filePath">Logical path or key under which the file will be stored.</param>
    /// <param name="file">The uploaded file.</param>
    /// <param name="overwrite">Indicates whether an existing file at the same path should be overwritten (not yet implemented).</param>
    /// <param name="tags">Optional metadata tags to store along with the file.</param>
    /// <returns>An <see cref="UploadResponse"/> describing the stored file.</returns>
    public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
    {
        if (file.Length > _options.FileSizeLimitInBytes)
            throw new InvalidOperationException($"File exceeds maximum allowed size of {_options.FileSizeLimitInMB} MB.");

        var metadata = new BsonDocument(tags ?? new Dictionary<string, string>())
        {
            {FilePathMetadataKey, filePath },
            {ContentTypeMetadataKey, file.ContentType },
        };

        var uploadOptions = new GridFSUploadOptions
        {
            Metadata = metadata,
            ChunkSizeBytes = _options.ChunkSizeBytes
        };

        ObjectId fileId;
        using (var stream = file.OpenReadStream())
        {
           fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);
        }

        return new UploadResponse(
            FilePath: filePath,
            FileName: file.FileName,
            FileSize: file.Length,
            FileId: fileId.ToString()
         );
    }

    /// <summary>
    /// Creates a new <see cref="FileService"/> using the provided GridFS bucket and options.
    /// </summary>
    /// <param name="bucket">GridFS bucket used to store file data.</param>
    /// <param name="options">Options controlling GridFS storage behavior.</param>
    public FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
        => (_bucket, _options) = (bucket, options.Value);

    /// <summary>
    /// Downloads a file stream and content type from GridFS by its identifier.
    /// </summary>
    /// <param name="fileId">Identifier of the file to download.</param>
    /// <returns>A tuple containing the file stream and its content type.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the id is invalid or the file does not exist.</exception>
    public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
    {
        if (!ObjectId.TryParse(fileId, out var objectId))
            throw new FileNotFoundException($"Invalid file ID: {fileId}");

        var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", fileId)).FirstOrDefaultAsync();
        if (fileId == null)
            throw new FileNotFoundException($"No file found with ID: {fileId}");

        var stream = await _bucket.OpenDownloadStreamAsync(fileId);
        var contentType = fileInfo.Metadata.GetValue(ContentTypeMetadataKey, DefaultContentType)?.AsString ?? DefaultContentType;

        return (stream, contentType);
    }

    /// <summary>
    /// Attempts to delete a file in GridFS by its identifier.
    /// </summary>
    /// <param name="fileId">Identifier of the file to delete.</param>
    /// <returns><c>true</c> if the file was deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> TryDeleteFileAsync(string fileId)
    {
        if (!ObjectId.TryParse(fileId, out var objectId))
            return false;

        try
        {
            await _bucket.DeleteAsync(objectId);
            return true;
        } catch(GridFSFileNotFoundException) { return false; }
    }

    
}
