///	Copyright		:	Copyright© LEADSOFT 2019. All rights reserved.
///	NameSpace		:	WebApi.Controllers
/// Class           :   IdentityController
///	Author			:	Mohammad Rakibul Hasan
///	Purpose			:	API Resource
///	Creation Date	:	27/11/19
/// ==================================================================================================
///  || Modification History ||
///  -------------------------------------------------------------------------------------------------
///  Sl No.	Date:		Author:			    Ver:	Area of Change:     
///  1.0    27/11/19  Rakib               1.0     Created
///	


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
      
        public ActionResult<string> Get()
        {
            //return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            return  "Hello from the API";
        }
    }
}