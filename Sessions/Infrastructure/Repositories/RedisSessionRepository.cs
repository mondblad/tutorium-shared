using StackExchange.Redis;
using System.Text.Json;
using Tutorium.Shared.Sessions.Abstractions;
using Tutorium.Shared.Sessions.Const;
using Tutorium.Shared.Sessions.Models;

namespace Tutorium.Shared.Sessions.Infrastructure.Repositories
{
    internal class RedisSessionRepository : ISessionRepository
    {
        private readonly IDatabase _db;

        public RedisSessionRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<Session?> GetByIdAsync(string sessionId)
        {
            var key = $"{SessionConstants.SessionPrefix}{sessionId}";
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? JsonSerializer.Deserialize<Session>(value!) : null;
        }

        public async Task SetAsync(Session session, ITransaction? transaction = null)
        {
            var key = $"{SessionConstants.SessionPrefix}{session.SessionId}";
            var value = JsonSerializer.Serialize(session);

            if (transaction is not null)
                await transaction.StringSetAsync(key, value, SessionConstants.Ttl);
            else
                await _db.StringSetAsync(key, value, SessionConstants.Ttl);
        }

        public async Task DeleteByIdAsync(string sessionId, ITransaction? transaction = null)
        {
            var key = $"{SessionConstants.SessionPrefix}{sessionId}";

            if (transaction is not null)
                await transaction.KeyDeleteAsync(key);
            else
                await _db.KeyDeleteAsync(key);
        }

        public async Task<List<Session>> GetByIdsAsync(IEnumerable<string> sessionIds)
        {
            if (!sessionIds.Any())
                return new List<Session>();

            var keys = sessionIds.Select(id => (RedisKey)$"{SessionConstants.SessionPrefix}{id}").ToArray();
            var values = await _db.StringGetAsync(keys);

            var sessions = new List<Session>();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].HasValue)
                {
                    var session = JsonSerializer.Deserialize<Session>(values[i]!);
                    if (session is not null)
                        sessions.Add(session);
                }
            }

            return sessions;
        }
    }
}
