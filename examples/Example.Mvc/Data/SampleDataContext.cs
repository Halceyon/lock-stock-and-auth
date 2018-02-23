using System.Data.Entity;
using Example.Mvc.Data.Entities;

namespace Example.Mvc.Data
{
    public class SampleDataContext : DbContext
    {
        public SampleDataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleDataContext, SampleDataContextConfiguration>());
        }

        public DbSet<Book> Books { get; set; }
    }
}
