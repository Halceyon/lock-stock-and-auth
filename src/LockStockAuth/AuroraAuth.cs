using Microsoft.AspNet.Identity;

namespace LockStockAuth
{
    public class AuroraAuth : OwinContextBase, IAuroraAuth
    {
        public void AddRole(string id, string roleName)
        {
            UserManager.AddToRole(id, roleName);
        }
    }
}
