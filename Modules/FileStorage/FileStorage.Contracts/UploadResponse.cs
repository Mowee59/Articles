namespace FileStorage.Contracts;

/// <summary>
/// Represents the result of a successful file upload operation.
/// </summary>
/// <param name="FilePath">Logical path or key under which the file is stored.</param>
/// <param name="FileName">Original name of the uploaded file.</param>
/// <param name="FileSize">Size of the uploaded file in bytes.</param>
/// <param name="FileId">Unique identifier assigned by the storage provider.</param>
public record class UploadResponse(string FilePath, string FileName, long FileSize, string FileId)
{
}
