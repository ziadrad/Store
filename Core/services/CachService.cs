﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction;

namespace services
{
    public class CachService (ICacheRepository cacheRepository) : ICachService
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value = await cacheRepository.GetAsync(key);
            return value == null ? null : value;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsync(key, value, duration);
        }
    }
}
