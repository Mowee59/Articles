using Blocks.Core;
using FileStorage.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;


namespace FileStorage.MongoGridFS;

/// <summary>
/// Service registration helpers for configuring MongoDB GridFS-based file storage.
/// </summary>
public static class FileStorageRegistration
{
    /// <summary>
    /// Registers MongoDB GridFS file storage components and the <see cref="IFileService"/>
    /// implementation using configuration bound to <see cref="MongoGridFsFileStorageOptions"/>.
    /// </summary>
    /// <param name="services">The service collection to register into.</param>
    /// <param name="configuration">Application configuration used for connection strings and options.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddMongoFileStorage(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAndValidateOptions<MongoGridFsFileStorageOptions>(configuration);
        var options = configuration.GetSectionByTypeName<MongoGridFsFileStorageOptions>();

        // Client
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(configuration.GetConnectionStringOrThrow(options.ConnectionStringName));
        });

        // Datanase
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName);
        });

        //Bucket
        services.AddSingleton(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return new GridFSBucket(db, new GridFSBucketOptions
            {
                BucketName = options.BucketName,
                ChunkSizeBytes = options.ChunkSizeBytes,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });
        });

        services.AddSingleton<IFileService, FileService>();

        return services;

    }
}
