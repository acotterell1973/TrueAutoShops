using System.Threading;
using System.Threading.Tasks;
using TrueAutoShops.Models;
using TrueAutoShops.Models.Response;

namespace TrueAutoShops.Services
{
    public interface ISecurityDataService
    {
        Task<TokenResponseModel> GetTokenTask();
        Task<TokenResponseModel> LoginUser(CancellationToken cancellationToken, Login login);

        Task<CreateProfileResponse> RegisterUser(CancellationToken cancellationToken, UserProfile user);
    }
}