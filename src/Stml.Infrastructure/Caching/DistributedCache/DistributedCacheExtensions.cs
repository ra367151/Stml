using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.System.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Caching.DistributedCache
{
    public static class DistributedCacheExtensions
    {
        public static TValue Get<TValue>(this IDistributedCache cache, [NotNull]string key)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            return cache.GetString(key).FromJsonString<TValue>();
        }

        public static async Task<TValue> GetAsync<TValue>(this IDistributedCache cache, [NotNull]string key, CancellationToken token = default)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            return (await cache.GetStringAsync(key, token)).FromJsonString<TValue>();
        }

        public static TValue Get<TValue>(this IDistributedCache cache, [NotNull]string key, Func<TValue> factory)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            var cacheItem = cache.GetString(key).FromJsonString<TValue>();
            if (cacheItem != null)
                return cacheItem;
            var item = factory();
            if (item != null)
            {
                cache.Set(key, item);
                return item;
            }
            return default;
        }

        public static async Task<TValue> GetAsync<TValue>(this IDistributedCache cache, [NotNull]string key, Func<TValue> factory, CancellationToken token = default)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            var cacheItem = (await cache.GetStringAsync(key, token)).FromJsonString<TValue>();
            if (cacheItem != null)
                return cacheItem;
            var item = factory();
            if (item != null)
            {
                await cache.SetAsync(key, item, token);
                return item;
            }
            return default;
        }

        public static TValue Get<TValue>(this IDistributedCache cache, [NotNull]string key, Func<TValue> factory, DistributedCacheEntryOptions options)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            var cacheItem = cache.GetString(key).FromJsonString<TValue>();
            if (cacheItem != null)
                return cacheItem;
            var item = factory();
            if (item != null)
            {
                cache.Set(key, item, options);
                return item;
            }
            return default;
        }

        public static async Task<TValue> GetAsync<TValue>(this IDistributedCache cache, [NotNull]string key, Func<TValue> factory, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            var cacheItem = (await cache.GetStringAsync(key, token)).FromJsonString<TValue>();
            if (cacheItem != null)
                return cacheItem;
            var item = factory();
            if (item != null)
            {
                await cache.SetAsync(key, item, options, token);
                return item;
            }
            return default;
        }

        public static void Set<TValue>(this IDistributedCache cache, [NotNull]string key, TValue value)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            cache.SetString(key, value.ToJsonString());
        }

        public static void Set<TValue>(this IDistributedCache cache, [NotNull]string key, TValue value, DistributedCacheEntryOptions options)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            cache.SetString(key, value.ToJsonString(), options);
        }

        public static async Task SetAsync<TValue>(this IDistributedCache cache, [NotNull]string key, TValue value, CancellationToken token = default)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            await cache.SetStringAsync(key, value.ToJsonString(), token);
        }

        public static async Task SetAsync<TValue>(this IDistributedCache cache, [NotNull]string key, TValue value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            Check.NotNullOrWhiteSpace(key, nameof(key));
            await cache.SetStringAsync(key, value.ToJsonString(), options, token);
        }

        public static void RefreshRange(this IDistributedCache cache, params string[] keys)
        {
            foreach (var key in keys)
            {
                cache.Refresh(key);
            }
        }

        public static async Task RefreshRangeAsync(this IDistributedCache cache, IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            if (keys != null && keys.Any())
            {
                foreach (var key in keys)
                {
                    await cache.RefreshAsync(key, cancellationToken);
                }
            }
        }

        public static void RemoveRange(this IDistributedCache cache, params string[] keys)
        {
            foreach (var key in keys)
            {
                cache.Remove(key);
            }
        }

        public static async Task RemoveRangeAsync(this IDistributedCache cache, IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            if (keys != null && keys.Any())
            {
                foreach (var key in keys)
                {
                    await cache.RemoveAsync(key, cancellationToken);
                }
            }
        }
    }
}
