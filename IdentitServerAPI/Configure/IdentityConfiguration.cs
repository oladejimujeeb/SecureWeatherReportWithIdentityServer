using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentitServerAPI.Configure
{
    public class IdentityConfiguration
    {
        public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1208",
                Username = "mujib",
                Password = "password",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Oladeji Mujib"),
                    new Claim(JwtClaimTypes.GivenName, "Mulad"),
                    new Claim(JwtClaimTypes.FamilyName, "Oladeji"),
                    new Claim(JwtClaimTypes.WebSite, "https://oladejimujib@gmail.com"),
                    new Claim(JwtClaimTypes.Issuer, "Mulad"),
                   // new Claim(JwtClaimTypes.Role, "Admin")
                },
            }
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
           /* new IdentityResource
            {
                Name = "Admin",
                UserClaims = new List<string>{"Adim"}
            }*/

        };
        //Anonymous Method
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
           new ApiScope("myApi.write"),
           new ApiScope("myApi.read")
        };

        public static IEnumerable<ApiResource> ApiResources()
        {
            ApiResource[] apiResources =
            {
                new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "myApi.read","myApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) },
                   // UserClaims = new List<string>{"Admin" }
                }
            };
            return apiResources;
        }

        public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "cwm.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "myApi.read","myApi.write" },
                AccessTokenLifetime = 900
            },

            //interactive client using code flow + pkce
           /* new Client
            {
                ClientId ="interactive",
                ClientSecrets = {new Secret ("secret".Sha256()) },
                AllowedGrantTypes= GrantTypes.Code,
                RedirectUris = {""},
                FrontChannelLogoutUri ="",
                PostLogoutRedirectUris ={""},
                AllowOfflineAccess = true,
                AllowedScopes ={"Openid","profile","myApi.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            }*/
        };
    }
}
