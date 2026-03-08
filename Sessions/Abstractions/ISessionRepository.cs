using StackExchange.Redis;
using Tutorium.Shared.Sessions.Models;

namespace Tutorium.Shared.Sessions.Abstractions
{
    internal interface ISessionRepository
    {
        Task<Session?> GetByIdAsync(string sessionId);
        Task SetAsync(Session session, ITransaction? transaction = null);
        Task DeleteByIdAsync(string sessionId, ITransaction? transaction = null);
        Task<List<Session>> GetByIdsAsync(IEnumerable<string> sessionIds);
    }
}
