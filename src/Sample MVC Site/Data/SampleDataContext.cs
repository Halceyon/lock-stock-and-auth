using System.Data.Entity;
using Sample_MVC_Site.Data.Entities;

namespace Sample_MVC_Site.Data
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
