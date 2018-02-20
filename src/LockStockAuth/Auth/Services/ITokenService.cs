using System.Threading.Tasks;
using LockStockAuth.Auth.Models;
using Newtonsoft.Json.Linq;

namespace LockStockAuth.Auth.Services
{
    public interface ITokenService
    {
        Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken);

        JObject GenerateLocalAccessTokenResponse(string userName);
    }
}
