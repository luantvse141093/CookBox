using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookingBox.Business.ViewModels;
using FirebaseAdmin.Auth;
using CookingBox.Business.IServices;
using CookingBox.Business.CustomEntities.ModelSearch;

namespace CookingBox.API.v1.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _usersService;

        public LoginController(IConfiguration configuration, IUserService usersService)
        {
            _configuration = configuration;
            _usersService = usersService;
        }
        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {

            if (IsValidUser(login).Result.Count > 0)
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();
        }
        List<KeyValuePair<string, string>> myList;
        private async Task<List<KeyValuePair<string, string>>> IsValidUser(UserLogin login)
        {
            myList = new List<KeyValuePair<string, string>>();
            if (login.token != null)
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(login.token);


                var email = decodedToken.Claims.GetValueOrDefault("email").ToString();

                myList.Add(new KeyValuePair<string, string>("name", decodedToken.Claims.GetValueOrDefault("name").ToString()));
                myList.Add(new KeyValuePair<string, string>("email", decodedToken.Claims.GetValueOrDefault("email").ToString()));
                myList.Add(new KeyValuePair<string, string>("role", "US"));

                var users = await _usersService.GetUsers(new UserListSearch
                {
                    email = email
                });

                if (users.Items.Count == 0)
                {
                    _usersService.InsertUser(new UserViewModel
                    {
                        email = email,
                        name = decodedToken.Claims.GetValueOrDefault("name").ToString(),
                        role_id = "US"
                    });
                }
            }
            else if (login.email != null && login.pass != null && login.token == null)
            {
                //if (_usersService.Login(login.email, login.pass) != null)
                //{
                //    myList.Add(new KeyValuePair<string, string>("name", _usersService.Login(login.email, login.pass).Result.name));
                //    myList.Add(new KeyValuePair<string, string>("email", _usersService.Login(login.email, login.pass).Result.email));
                //    myList.Add(new KeyValuePair<string, string>("role", _usersService.Login(login.email, login.pass).Result.role_name));
                //}
                myList.Add(new KeyValuePair<string, string>("name", "luan"));
                myList.Add(new KeyValuePair<string, string>("email", "luan@gmail.com"));
                myList.Add(new KeyValuePair<string, string>("role", "AD"));

            }
            return myList;
        }

        private string GenerateToken()
        {


            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, myList[0].Value),
                new Claim(ClaimTypes.Email,myList[1].Value ),
                new Claim(ClaimTypes.Role, myList[2].Value),
            };
            var payload = new JwtPayload
                (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddHours(2)

                );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}