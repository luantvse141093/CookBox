using AutoMapper;
using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.ViewModels;
using CookingBox.Business.IServices;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using CookingBox.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CookingBox.Api.Controllers.User
{
    [Route("api/v1/user/accounts")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IUriService _uriService;

        public AccountsController(IUserService usersService, IUriService uriService)
        {
            _usersService = usersService;
            _uriService = uriService;

        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _usersService.GetUserByEmail(email);
            if (user != null)
            {
                //var response = new ApiResponse<UserDto>(userDTO);
                return Ok(user);
            }
            return NotFound();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _usersService.GetUser(userViewModel.id);
            user.name = userViewModel.name;
            user.address = userViewModel.address;
            user.phone = userViewModel.phone;

            var result = await _usersService.UpdateUser(user);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }


    }
}
