using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TeamXSecurityClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> LoginAsync([FromBody]AppUser testUser)
        {

            var user = new AppUser();

            user.UserName = "Rakib";
            user.Password = "12345";

            if (testUser.UserName == user.UserName && testUser.Password == user.Password)
            {
                string finalToken = "";
                //// discover endpoints from the metadata by calling Auth server hosted on 5000 port
                var discover = await DiscoveryClient.GetAsync("http://localhost:5000");
                if (discover.IsError)
                {
                    Console.WriteLine(discover.Error);

                }

                // request token
                var tokenClient = new TokenClient(discover.TokenEndpoint, "client", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);

                }

                Console.WriteLine(tokenResponse.Json);

                finalToken = tokenResponse.Json.ToString();

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

                    finalToken += JArray.Parse(content).ToString();

                    /*return Ok(JArray.Parse(content));*/
                }

                return Ok(finalToken);
            }

            else
            {
                return Unauthorized();
            }

           
        }
    }
}