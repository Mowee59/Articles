using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

/// <summary>
/// Repository for <see cref="AssetTypeDefinition"/> entities, backed by
/// <see cref="SubmissionDbContext"/> and using in-memory caching keyed by <see cref="AssetType"/>.
/// </summary>
public class AssetTypeDefinitionRepository(SubmissionDbContext dbContext, IMemoryCache cache) 
    : CachedRepository<SubmissionDbContext, AssetTypeDefinition, AssetType>(dbContext, cache)
{
}
