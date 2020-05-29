using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToysNGames.Data.Entities;

namespace ToysNGames.Data
{
    public class DBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        { }

        public void DetachAllEntities()
        {
            if (ChangeTracker != null)
            {
                var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Detached ||
                            e.State == EntityState.Unchanged)
                .ToList();

                foreach (var entry in changedEntriesCopy)
                    entry.State = EntityState.Detached;
            }

        }
    }
}
