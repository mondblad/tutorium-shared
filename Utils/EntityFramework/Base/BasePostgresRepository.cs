using Microsoft.EntityFrameworkCore;

namespace Tutorium.Shared.Utils.EntityFramework.Base
{
    public abstract class BasePostgresRepository<TContext> where TContext : DbContext
    {
        protected TContext Context;

        public BasePostgresRepository(TContext context) { Context = context; }
    }
}
