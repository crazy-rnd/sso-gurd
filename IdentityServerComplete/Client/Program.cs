using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        public static void Main(string[] args) { MainAsync(args).GetAwaiter().GetResult(); }

        static async System.Threading.Tasks.Task MainAsync(string[] args)
        {
            Console.WriteLine("Hello World!");

            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);

                return;
            }

            //// request token without credential
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}

            //Console.WriteLine(tokenResponse.Json);

            // request token with credential
            var client = new HttpClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client2 = new HttpClient();

            client2.SetBearerToken(tokenResponse.AccessToken);

            var response = await client2.GetAsync("http://localhost:5001/api/identity");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));


        }
    }
}
