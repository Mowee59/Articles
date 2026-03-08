namespace FileStorage.Contracts;

public record class UploadResponse(string FilePath, string FileName, long FileSize, string FileId)
{
}
