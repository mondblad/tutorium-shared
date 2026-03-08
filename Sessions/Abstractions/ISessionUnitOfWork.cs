using StackExchange.Redis;

namespace Tutorium.Shared.Sessions.Abstractions
{
    internal interface ISessionUnitOfWork
    {
        ITransaction BeginTransaction();
        Task CommitAsync(ITransaction tran);
    }
}
