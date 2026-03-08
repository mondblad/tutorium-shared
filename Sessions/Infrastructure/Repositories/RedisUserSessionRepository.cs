using StackExchange.Redis;
using System.Text.Json;
using Tutorium.Shared.Sessions.Abstractions;
using Tutorium.Shared.Sessions.Const;
using Tutorium.Shared.Sessions.Models;

namespace Tutorium.Shared.Sessions.Infrastructure.Repositories
{
    internal class RedisUserSessionRepository : IUserSessionRepository
    {
        private readonly IDatabase _db;

        public RedisUserSessionRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<UserSessions?> GetByIdAsync(int userId)
        {
            var key = $"{SessionConstants.UserSessionsPrefix}{userId}";
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? JsonSerializer.Deserialize<UserSessions>(value!) : null;
        }

        public async Task SetAsync(UserSessions userSession, ITransaction? transaction = null)
        {
            var key = $"{SessionConstants.UserSessionsPrefix}{userSession.UserId}";
            var value = JsonSerializer.Serialize(userSession);

            if (transaction is not null)
                await transaction.StringSetAsync(key, value);
            else 
                await _db.StringSetAsync(key, value);
        }

        public async Task DeleteByIdAsync(int userSessionId, ITransaction? transaction = null)
        {
            var key = $"{SessionConstants.UserSessionsPrefix}{userSessionId}";
            await _db.KeyDeleteAsync(key);

            if (transaction is not null)
                await transaction.KeyDeleteAsync(key);
            else
                await _db.KeyDeleteAsync(key);
        }
    }
}
