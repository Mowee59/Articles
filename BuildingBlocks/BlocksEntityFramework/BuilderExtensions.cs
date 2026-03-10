using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

/// <summary>
/// Extension methods and helpers for configuring EF Core model builders.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Configures an enum property to be stored as its string representation in the database.
    /// </summary>
    /// <typeparam name="TEnum">Enum type of the property.</typeparam>
    /// <param name="builder">The property builder.</param>
    /// <returns>The same property builder for chaining.</returns>
    public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder) where TEnum : Enum
    {
        return builder.HasConversion(
            v => v.ToString(),
            v => (TEnum)Enum.Parse(typeof(TEnum), v)
         );
    }

    /// <summary>
    /// Configures a property to be stored as JSON in a single string column,
    /// using the default JSON serializer options.
    /// </summary>
    /// <typeparam name="T">Type of the property to serialize.</typeparam>
    /// <param name="builder">The property builder.</param>
    /// <returns>The same property builder for chaining.</returns>
    public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder)
    {
        return builder.HasConversion(BuildJsonListConvertor<T>());
    }

    /// <summary>
    /// Creates a <see cref="ValueConverter{TModel, TProvider}"/> that serializes and deserializes
    /// a value to and from JSON stored in a string column.
    /// </summary>
    /// <typeparam name="TCollection">Type of the value to serialize.</typeparam>
    /// <returns>A JSON-based value converter.</returns>
    public static ValueConverter<TCollection, string> BuildJsonListConvertor<TCollection>()
    {

        Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
        Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]") ;

        return new ValueConverter<TCollection, string>(
            v => serializeFunc(v),
            v => deserializeFunc(v)
        );
    }

    /// <summary>
    /// Configures a property to be stored with the same name as the property in the database.
    /// </summary>
    /// <typeparam name="TProperty">Type of the property to configure.</typeparam>
    /// <param name="builder">The property builder.</param>
    /// <returns>The same property builder for chaining.</returns>
    public static PropertyBuilder<TProperty> HasColumnNameAsProperty<TProperty>(this PropertyBuilder<TProperty> builder)
        => builder.HasColumnName(builder.Metadata.PropertyInfo?.Name);
}
