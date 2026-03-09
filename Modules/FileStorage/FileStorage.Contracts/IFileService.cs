using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

/// <summary>
/// Contract for a file storage service responsible for uploading, downloading,
/// and deleting files in a backing store (e.g. cloud or local storage).
/// </summary>
public interface  IFileService
{
    /// <summary>
    /// Uploads a file to the storage provider.
    /// </summary>
    /// <param name="filePath">Logical path or key under which the file will be stored.</param>
    /// <param name="file">The file content received from the client.</param>
    /// <param name="overwrite">Indicates whether an existing file at the same path should be overwritten.</param>
    /// <param name="tags">Optional metadata tags to associate with the stored file.</param>
    /// <returns>Information about the uploaded file.</returns>
    Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null);

    /// <summary>
    /// Downloads a file from storage by its identifier.
    /// </summary>
    /// <param name="fileId">Identifier or key of the file to download.</param>
    /// <returns>A tuple containing the file stream and its content type.</returns>
    Task<(Stream FileStream, string ContentType)>DownloadFileAsync(string fileId);

    /// <summary>
    /// Attempts to delete a file from storage.
    /// </summary>
    /// <param name="fileId">Identifier or key of the file to delete.</param>
    /// <returns><c>true</c> if the file was deleted; otherwise, <c>false</c>.</returns>
    Task<bool> TryDeleteFileAsync(string fileId);

}
