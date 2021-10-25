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

namespace CookingBox.Api.Controllers.Admin
{
    [Authorize(Policy = "AD")]
    [Route("api/v1/admin/users")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IUriService _uriService;

        public UsersController(IUserService usersService, IUriService uriService)
        {
            _usersService = usersService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserListSearch userListSearch)
        {
            var users = await _usersService.GetUsers(userListSearch);
            if (users.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<UserViewModel>.CreatePagedReponse(users, _uriService,
              string.Concat(Request.Path.Value, Request.QueryString.Value)
              );

                //var response = new ApiResponse<IEnumerable<UserDto>>(usersDTO);
                return Ok(pageResponse);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _usersService.GetUser(id);
            if (user != null)
            {
                //var response = new ApiResponse<UserDto>(userDTO);
                return Ok(user);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserViewModel userViewModel)
        {

            try
            {
                int id = await _usersService.InsertUser(userViewModel);
                return Ok(id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            var result = await _usersService.UpdateUser(userViewModel);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usersService.DeleteUser(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }

    }
}
