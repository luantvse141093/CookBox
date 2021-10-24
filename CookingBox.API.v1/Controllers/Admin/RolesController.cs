using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.ViewModels;
using CookingBox.Business.IServices;
using CookingBox.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/roles")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly IUriService _uriService;

        public RolesController(IRoleService roleService, IUriService uriService)
        {
            _roleService = roleService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] RoleListSearch roleListSearch)
        {
            var roles = await _roleService.GetRoles(roleListSearch);
            if (roles.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<RoleViewModel>.CreatePagedReponse(roles, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                //var response = new ApiResponse<IEnumerable<RoleDto>>(RolesDTO);
                return Ok(pageResponse);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleService.GetRole(id);
            //var response = new ApiResponse<RoleDto>(RoleDTO);
            if (role != null)
            {
                return Ok(role);
            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> InsertRole([FromBody] RoleViewModel roleViewModel)
        {

            try
            {
                await _roleService.InsertRole(roleViewModel);
                return Ok(true);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleViewModel roleViewModel)
        {

            var result = await _roleService.UpdateRole(roleViewModel);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleService.DeleteRole(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
