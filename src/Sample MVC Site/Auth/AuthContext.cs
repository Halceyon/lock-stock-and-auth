using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Sample_MVC_Site.Auth.Entities;
using Sample_MVC_Site.Auth.Managers;
using Sample_MVC_Site.Auth.Models;

namespace Sample_MVC_Site.Auth
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("DefaultConnection")
        {

        }

        public static AuthContext Create()
        {
            return new AuthContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}