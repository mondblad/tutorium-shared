using StackExchange.Redis;
using Tutorium.Shared.Sessions.Abstractions;

namespace Tutorium.Shared.Sessions.Infrastructure
{
    internal class SessionUnitOfWork : ISessionUnitOfWork
    {
        private readonly IDatabase _database;

        public SessionUnitOfWork(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public ITransaction BeginTransaction() => _database.CreateTransaction();

        public async Task CommitAsync(ITransaction tran)
        {
            bool success = await tran.ExecuteAsync();
            if (!success)
                throw new Exception("Не удалось выполнить атомарную операцию");
        }
    }
}
