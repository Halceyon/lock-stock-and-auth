using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using Sample_MVC_Site.Auth;
using Sample_MVC_Site.Auth.Entities;
using Sample_MVC_Site.Data.Entities;

namespace Sample_MVC_Site.Data
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

        private static IEnumerable<Client> BuildClientsList()
        {

            var clientsList = new List<Client>
            {
                new Client
                {
                    Id = "jsApp",
                    Secret = GetHash("LookIamSecure"),
                    Name = "Javascript Application",
                    ApplicationType = ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret = GetHash("LookIamSecure"),
                    Name = "Console Application",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return clientsList;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            var byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            var byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
