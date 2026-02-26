using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Tutorium.Shared.Utils.EntityFramework.Abstractions;
using System.Linq.Expressions;

namespace Tutorium.Shared.Utils.EntityFramework.Base
{
    public abstract class BasePostgresCRUDRepository<T, TContext> : BasePostgresRepository<TContext>, IPostgresCRUDRepository<T> 
        where T : class
        where TContext : DbContext
    {
        protected DbSet<T> Set => Context.Set<T>();

        public BasePostgresCRUDRepository(TContext context) : base(context) { }

        public void RemoveRange(IEnumerable<T> models) => Set.RemoveRange(models);

        public void Remove(T model) => Set.Remove(model);

        public void Add(T model) => Set.Add(model);

        public void AddRange(IEnumerable<T> models) => Set.AddRange(models);

        public void Attach(T entity) => Set.Attach(entity);
        
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) => await Set.AnyAsync(predicate, ct);

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate) => Set.Where(predicate);

        public async Task SaveChangesAsync(CancellationToken ct = default) => await Context.SaveChangesAsync(ct);

        protected EntityEntry<T> Entry(T entity) => Set.Entry(entity);
    }
}
