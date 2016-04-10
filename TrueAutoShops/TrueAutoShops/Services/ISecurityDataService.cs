using System.Threading.Tasks;
using TrueAutoShops.Models;

namespace TrueAutoShops.Services
{
    public interface ISecurityDataService
    {
        Task<TokenResponseModel> GetTokenTask();
        Task<TokenResponseModel> LoginUser(Login login);

        Task<string> RegisterUser(UserProfile user);
    }
}