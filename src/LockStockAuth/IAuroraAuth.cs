using LockStockAuth.Auth.Managers;
using Microsoft.Owin.Security;

namespace LockStockAuth
{
    public interface IAuroraAuth
    {
        void AddRole(string id, string roleName);
        IAuthenticationManager Authentication { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
    }
}