using IdentityModel.Client;
using System.Threading.Tasks;

namespace WebAppWeather.Services.Interface
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
