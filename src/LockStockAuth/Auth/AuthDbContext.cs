using System.Data.Entity;
using LockStockAuth.Auth.Entities;
using LockStockAuth.Auth.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LockStockAuth.Auth
{
    public class AuthDbContext : IdentityDbContext<LockStockUser>
    {
        public AuthDbContext(string connectionNameOrConnectionString)
            : base(connectionNameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthDbContext, AuthDbContextConfiguration>(connectionNameOrConnectionString));
        }
        public AuthDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthDbContext, AuthDbContextConfiguration>("DefaultConnection"));
        }

        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
    }

}
