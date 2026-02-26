using System.Linq.Expressions;

namespace Tutorium.Shared.Utils.EntityFramework.Abstractions
{
    public interface IPostgresCRUDRepository<T> where T : class
    {
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Attach(T entity);

        Task SaveChangesAsync(CancellationToken ct = default);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
