using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/menus")]
    //[Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IUriService _uriService;

        public MenusController(IMenuService menuService, IUriService uriService)
        {
            _menuService = menuService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMenus([FromQuery] MenuListSearch menuListSearch)
        {
            var menus = await _menuService.GetMenus(menuListSearch);
            if (menus.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<MenuViewModel>.CreatePagedReponse(menus, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                return Ok(pageResponse);

            }
            return NotFound();

            //var response = new ApiResponse<IEnumerable<DishDto>>(DishsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            var menu = await _menuService.GetMenu(id);
            if (menu != null)
            {
                //var response = new ApiResponse<DishDto>(DishDTO);
                return Ok(menu);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> InsertMenu([FromBody] MenuViewModel menuViewModel)
        {
            try
            {

                await _menuService.InsertMenu(menuViewModel);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu(MenuViewModel menuViewModel)
        {

            var result = await _menuService.UpdateMenu(menuViewModel);
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
            var result = await _menuService.DeleteMenu(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
