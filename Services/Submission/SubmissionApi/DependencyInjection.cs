using FileStorage.MongoGridFS;

namespace Submission.API;

/// <summary>
/// Service registration helpers for the Submission API layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers API-related services such as memory caching, Swagger/minimal API exploration,
    /// and MongoDB-based file storage for the Submission service.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Application configuration used for file storage setup.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddMemoryCache()  // Basic Caching
            .AddEndpointsApiExplorer() // Minimal API Doc exploraiton for Swagger
            .AddSwaggerGen() // Swagger setup
            ;

        services.AddMongoFileStorage(configuration);
        return services;
    }
}
