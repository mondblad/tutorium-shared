using Microsoft.EntityFrameworkCore;
using Tutorium.Shared.Utils.EntityFramework.Extensions;

namespace Tutorium.Shared.Utils.EntityFramework.Base
{
    public abstract class BasePgContext : DbContext
    {
        public BasePgContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ModelBuilderExtensions.ApplySeparateTableAttribute(modelBuilder);
        }
    }
}
