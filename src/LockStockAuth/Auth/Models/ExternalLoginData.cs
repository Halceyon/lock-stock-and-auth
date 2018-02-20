using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace LockStockAuth.Auth.Models
{
    public class ExternalLoginData
    {
        public string Email { get; set; }
        public string ExternalAccessToken { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }

        public string ShareRef { get; set; }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity, HttpRequestMessage request)
        {
            var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            var shareRef = request.GetQueryNameValuePairs().SingleOrDefault(q => q.Key == "share_ref").Value;
            var role = request.GetQueryNameValuePairs().SingleOrDefault(q => q.Key == "role").Value;

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirstValue(ClaimTypes.Email),
                Email = identity.FindFirstValue(ClaimTypes.Email),
                Name = identity.FindFirstValue(ClaimTypes.Name),
                ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                Role = role,
                ShareRef = shareRef
            };
        }

        public string Role { get; set; }
    }
}
