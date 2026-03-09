using Blocks.Core.Extensions;
using FluentValidation;

namespace Blocks.Core.FluentValidation;

/// <summary>
/// Extension methods for composing FluentValidation rules with standardized error messages.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds a standardized "invalid id" message to the rule.
    /// </summary>
    /// <typeparam name="T">Type being validated.</typeparam>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="rule">The rule builder options to decorate.</param>
    /// <param name="propertyName">Name of the property used in the error message.</param>
    /// <returns>Updated rule builder options.</returns>
    public static IRuleBuilderOptions<T, TProperty> WithMessageForInvalidId<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string propertyName)
        => rule.WithMessage( c => ValidationMessages.InvalidId.FormatWith(propertyName));

    /// <summary>
    /// Adds a not-empty rule with a standardized "null or empty" message.
    /// </summary>
    /// <typeparam name="T">Type being validated.</typeparam>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <param name="propertyName">Name of the property used in the error message.</param>
    /// <returns>Updated rule builder options.</returns>
    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string propertyName)
        => ruleBuilder.NotEmpty()
               .WithMessage( c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));

    /// <summary>
    /// Adds a maximum length rule for strings with a standardized "max length exceeded" message.
    /// </summary>
    /// <typeparam name="T">Type being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <param name="maxLength">Maximum allowed length.</param>
    /// <param name="propertyName">Name of the property used in the error message.</param>
    /// <returns>Updated rule builder options.</returns>
    public static IRuleBuilderOptions<T, string?> MaximumLengthWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength, string propertyName)
        => ruleBuilder
            .MaximumLength(maxLength)
            .WithMessage( c => ValidationMessages.MaxLengthExceeded.FormatWith(propertyName));

    /// <summary>
    /// Adds a not-null rule with a standardized "null or empty" message based on the property type name.
    /// </summary>
    /// <typeparam name="T">Type being validated.</typeparam>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <returns>Updated rule builder options.</returns>
    public static IRuleBuilderOptions<T, TProperty> NotNullWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        => ruleBuilder
            .NotNull()
            .WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(typeof(TProperty).Name));

}
