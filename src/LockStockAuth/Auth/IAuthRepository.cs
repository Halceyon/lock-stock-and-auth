using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LockStockAuth.Auth.Entities;
using LockStockAuth.Auth.Models;
using Microsoft.AspNet.Identity;

namespace LockStockAuth.Auth
{
    public interface IAuthRepository: IDisposable
    {
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<LockStockUser> FindUser(string userName, string password);
        Client FindClient(string clientId);
        Task<IdentityResult> AddToRole(string userId, string roleName);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
        Task<LockStockUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(LockStockUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}
