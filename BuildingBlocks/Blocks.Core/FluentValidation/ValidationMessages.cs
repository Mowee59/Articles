
namespace Blocks.Core.FluentValidation;

internal class ValidationMessages
{
    public static readonly string InvalidId = "The {0} must be greater than 0.";
    public static readonly string MaxLengthExceeded = "{0} must be at most {1} characters long.";
    public static readonly string NullOrEmptyValue = "{0} is required.";
}
