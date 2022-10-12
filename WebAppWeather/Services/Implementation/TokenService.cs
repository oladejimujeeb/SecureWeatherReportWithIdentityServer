using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppWeather.Services.Interface;

namespace WebAppWeather.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
       

        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:5001/.well-known/openid-configuration").Result;
            }
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            using HttpClient client = new();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync
                (
                    new ClientCredentialsTokenRequest
                    {
                        ClientSecret = "secret",
                        Address = _discDocument.TokenEndpoint,
                        ClientId = "cwm.client",
                        Scope = scope,
                    }
                );

            if (tokenResponse.IsError)
                throw new Exception("Token Error");

            return tokenResponse;
        }
    }
}
