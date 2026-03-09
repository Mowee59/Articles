namespace Blocks.Domain.ValueObjects;

/// <summary>
/// Base class for string-based value objects that encapsulate domain concepts
/// (e.g. <c>Email</c>, <c>ArticleTitle</c>, <c>Slug</c>).
/// </summary>
public abstract class StringValueObject : IEquatable<StringValueObject>, IEquatable<string>
{
    /// <summary>
    /// Underlying string value for the value object.
    /// </summary>
    public string Value { get; protected set; } = default!;

    /// <summary>
    /// Returns the underlying string representation.
    /// </summary>
    public override string ToString() => Value.ToString();

    /// <summary>
    /// Uses the underlying string hash code to support collections and dictionaries.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Compares two value objects by their underlying string values.
    /// </summary>
    public bool Equals(StringValueObject? other) => Value.Equals(other?.Value);

    /// <summary>
    /// Compares the value object directly to a raw string.
    /// </summary>
    public bool Equals(string? other) => Value.Equals(other);

    /// <summary>
    /// Allows implicit conversion from the value object to its underlying string.
    /// </summary>
    public static implicit operator string(StringValueObject valueObject) => valueObject.Value;
}
