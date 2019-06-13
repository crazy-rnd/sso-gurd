using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamXSecurityAuthServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            // client credentials, list of clients
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
 
                    // Client secrets
                    ClientSecrets ={ new Secret ("secret".Sha256()) },
                    AllowedScopes = { "api1" }
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>{ new ApiResource("api1", "My API") };
        }
    }
}
