using StackExchange.Redis;
using System.Text.Json;
using Tutorium.Shared.Utils.Redis.Abstractions;
//using Newtonsoft.Json;

namespace Tutorium.Shared.Utils.Redis.Base
{
    public class BaseRedisRepository<T> : IRuntimeRepository<T> where T : class, IWithGuidToken
    {
        private readonly IDatabase _db;

        public BaseRedisRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task Add(T model) => await SaveOrUpdateAsync(model);
        public async Task Update(T model) => await SaveOrUpdateAsync(model);

        public async Task<T?> GetByTokenAsync(Guid token)
        {
            var value = await _db.StringGetAsync(GetKey(token));
            if (!value.HasValue)
                return null;

            return JsonSerializer.Deserialize<T>(value!);
        }

        private async Task SaveOrUpdateAsync(T attempt)
        {
            var json = JsonSerializer.Serialize(attempt);

            await _db.StringSetAsync(GetKey(attempt), json, new TimeSpan(0, 10, 0));
        }

        private string GetKey(T attempt)
        {
            return GetKey(attempt.Token);
        }

        private string GetKey(Guid token)
        {
            return $"{typeof(T).Name}:{token:N}";
        }
    }
}
