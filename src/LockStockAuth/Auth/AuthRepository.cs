using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockStockAuth.Auth.Entities;
using LockStockAuth.Auth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LockStockAuth.Auth
{
    public class AuthRepository : IDisposable, IAuthRepository
    {
        private readonly AuthDbContext _ctx;

        public UserManager<LockStockUser> UserManager;

        public AuthRepository()
        {
            _ctx = new AuthDbContext();
            UserManager = new UserManager<LockStockUser>(new UserStore<LockStockUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new LockStockUser
            {
                UserName = userModel.UserName
            };

            var result = await UserManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<LockStockUser> FindUser(string userName, string password)
        {
            LockStockUser user = await UserManager.FindAsync(userName, password);

            return user;
        }

        public virtual Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<IdentityResult> AddToRole(string userId, string roleName)
        {
            return await UserManager.AddToRoleAsync(userId, roleName);
        }
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _ctx.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _ctx.RefreshTokens.Remove(refreshToken);
               return await _ctx.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
             return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _ctx.RefreshTokens.ToList();
        }

        public async Task<LockStockUser> FindAsync(UserLoginInfo loginInfo)
        {
            LockStockUser user = await UserManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(LockStockUser user)
        {
            var result = await UserManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await UserManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            UserManager.Dispose();

        }
    }
}
