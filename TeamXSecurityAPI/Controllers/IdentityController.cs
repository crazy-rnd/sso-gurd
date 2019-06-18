using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace TeamXSecurityAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });

            
        }

        [HttpGet]
        [Route("GetFromApi2")]
        public async Task<IActionResult> GetFromApi2Async()
        {
            var discover = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discover.IsError)
            {
                Console.WriteLine(discover.Error);

            }

            // request token
            //var tokenClient = new TokenClient(discover.TokenEndpoint, "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");


            //test

            var re = Request;
            var headers = re.Headers;

            StringValues x = default(StringValues);

            headers.TryGetValue("Authorization", out x);
            string token = x.ToString();

            string finalToken = token.Substring(7);


            // call api
            string message = "";

            var client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);
            client.SetBearerToken(finalToken);

            var response = await client.GetAsync("http://localhost:26050/api/values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));

                message += JArray.Parse(content).ToString();

                /*return Ok(JArray.Parse(content));*/
            }

            return Ok(message);
        }
    }
}
