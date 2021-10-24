using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


namespace CookingBox.API.v1.Controllers
{
    [Route("api/v1/accountinfo")]
    [ApiController]
    public class AccountInfoController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetInfo(string jwtToken)
        {

            var jwt = jwtToken;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);

            string name = jwtSecurityToken.Claims
                .First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            string email = jwtSecurityToken.Claims
                .First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            string role_id = jwtSecurityToken.Claims
                .First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            var tokenInfo = new Dictionary<string, string>();
            tokenInfo.Add("name", name);
            tokenInfo.Add("email", email);
            tokenInfo.Add("role_id", role_id);

            return Ok(tokenInfo);
        }


    }
}
