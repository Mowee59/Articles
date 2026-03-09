using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core;

/// <summary>
/// Extension methods for working with application configuration and options.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Registers <typeparamref name="TOptions"/> in the DI container, binding it from a configuration
    /// section named after the type and validating it using data annotations. Fails fast on startup
    /// if the section is missing or invalid.
    /// </summary>
    /// <typeparam name="TOptions">Options type to bind and validate.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddAndValidateOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
    {
        var section = configuration.GetSection(typeof(TOptions).Name);

        if (!section.Exists())
            throw new InvalidOperationException($"Configuration section '{section.Key}' is missing.");

        services
            .AddOptions<TOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart(); // Fail fast if config is wrong

        return services;
    }

    /// <summary>
    /// Retrieves and binds a configuration section named after <typeparamref name="T"/>,
    /// throwing if the section is missing or cannot be bound.
    /// </summary>
    /// <typeparam name="T">Type to bind the configuration section to.</typeparam>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The bound configuration object.</returns>
    public static T GetSectionByTypeName<T>(this IConfiguration configuration)
    {
        var sectionName = typeof(T).Name;
        var section = configuration.GetSection(sectionName).Get<T>();

        return Guard.AgainstNull(section, sectionName);
    }

    /// <summary>
    /// Retrieves a named connection string or throws if it is missing or empty.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="name">Name of the connection string.</param>
    /// <returns>The connection string value.</returns>
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        var value = configuration.GetConnectionString(name);
        if (value == null)
            throw new InvalidOperationException($"Connection string '{name}' is msiising or empty.");

        return value;
    }
}
