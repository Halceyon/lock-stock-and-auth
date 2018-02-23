using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Example.Mvc.Data.Entities;

namespace Example.Mvc.Data
{
    public class SampleDataContextConfiguration : DbMigrationsConfiguration<SampleDataContext>
    {
        public SampleDataContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SampleDataContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "Moby Dick",
                    Author = "Herman Melville"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Hamlet",
                    Author = "William Shakespeare"
                }
            });
            context.SaveChanges();
        }
    }
}
