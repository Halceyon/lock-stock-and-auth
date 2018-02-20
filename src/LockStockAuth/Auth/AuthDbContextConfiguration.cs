using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using LockStockAuth.Auth.Entities;
using LockStockAuth.Auth.Managers;
using LockStockAuth.Auth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LockStockAuth.Auth
{
    internal sealed class AuthDbContextConfiguration : DbMigrationsConfiguration<AuthDbContext>
    {
        public AuthDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AuthDbContext dbContext)
        {
            if (!dbContext.Clients.Any())
            {
                dbContext.Clients.AddRange(BuildClientsList());
                dbContext.SaveChanges();
            }

            // Seed roles
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(dbContext));

            if (!roleManager.Roles.Any(r => r.Name == "Administrators"))
            {
                roleManager.Create(new IdentityRole("Administrators"));
            }

            // Seed admin user
            var userManager = new ApplicationUserManager(new UserStore<LockStockUser>(dbContext));

            var demoUserId = Guid.NewGuid().ToString();
            var demoEmail = ConfigurationManager.AppSettings["lockstockauth:DemoEmail"];
            if (!userManager.Users.Any(u => u.UserName == demoEmail))
            {
                userManager.Create(new LockStockUser()
                {
                    Id = demoUserId,
                    Email = ConfigurationManager.AppSettings["lockstockauth:DemoEmail"],
                    UserName = ConfigurationManager.AppSettings["lockstockauth:DemoEmail"],
                    EmailConfirmed = true
                }, ConfigurationManager.AppSettings["lockstockauth:DemoPassword"]);
            }

            base.Seed(dbContext);
        }

        private static IEnumerable<Client> BuildClientsList()
        {

            var clientsList = new List<Client>
            {
                new Client
                {
                    Id = "jsApp",
                    Secret = GetHash(ConfigurationManager.AppSettings["lockstockauth:ClientSecretHash"]),
                    Name = "Javascript Application",
                    ApplicationType = ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "mobileApp",
                    Secret = GetHash(ConfigurationManager.AppSettings["lockstockauth:ClientSecretHash"]),
                    Name = "Mobile Application",
                    ApplicationType = ApplicationTypes.Mobile,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
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
