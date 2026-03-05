using Blocks.Core.Extensions;

namespace Submission.Domain.ValueObjects;

public class FileExtensions
{
    public IReadOnlyList<string> Extensions { get; init; } = null!;

    public bool IsValidExtension(string extension)
        // NOTE : an emptuy extenion list means that all extensions are allowed
        => extension.IsEmpty() || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
}
