using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;

namespace Presistants.Repositories
{
    public class CachRepository (IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var value = await database.StringGetAsync(key);
            return !value.IsNullOrEmpty ? value : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var redisValue = JsonSerializer.Serialize(value);
            await database.StringSetAsync(key, redisValue, duration);
        }
    }
}
