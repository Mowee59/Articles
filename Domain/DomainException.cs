namespace Blocks.Domain;

/// <summary>
/// Exception type for domain rule violations and invariant failures.
/// Use this to signal business logic errors rather than technical failures.
/// </summary>
public class DomainException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{
}
