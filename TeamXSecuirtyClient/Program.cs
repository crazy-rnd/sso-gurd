using IdentityModel.Client;
using System;

using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace TeamXSecuirtyClient
{
    class Program
    {
        public static void Main(string[] args) { MainAsync(args).GetAwaiter().GetResult(); }

        static async Task MainAsync(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //// discover endpoints from the metadata by calling Auth server hosted on 5000 port
            //var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000");
            //if (discoveryClient.IsError)
            //{
            //    Console.WriteLine(discoveryClient.Error);
            //    return;
            //}

            //// request the token from the Auth server
            //var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "client", "secret");
            //var response = await tokenClient.RequestClientCredentialsAsync("api1");

            //if (response.IsError)
            //{
            //    Console.WriteLine(response.Error);
            //    return;
            //}

            var discover = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discover.IsError)
            {
                Console.WriteLine(discover.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(discover.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            //Console.WriteLine(response.Json);

            //var model = new User();
        }
    }
}
