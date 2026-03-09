using Microsoft.Extensions.Caching.Memory;

namespace Blocks.Core.Cache;

/// <summary>
/// Extension methods for working with <see cref="IMemoryCache"/>.
/// </summary>
public static class MemoryCacheExtensions
{
    /// <summary>
    /// Gets a cached instance of <typeparamref name="T"/> or creates it using
    /// the provided <paramref name="factory"/>. The cache key is the full type name.
    /// </summary>
    /// <typeparam name="T">Type of the cached value.</typeparam>
    /// <param name="memoryCache">The memory cache instance.</param>
    /// <param name="factory">Factory used to create and configure the cache entry.</param>
    /// <returns>The cached or newly created instance of <typeparamref name="T"/>.</returns>
    public static T GetOrCreateByType<T>(this IMemoryCache memoryCache, Func<ICacheEntry, T> factory)
        => memoryCache.GetOrCreate(typeof(T).FullName!, factory)!;
}
