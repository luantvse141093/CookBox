using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.ModelSearch.User;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using CookingBox.Business.ViewModels.User;
using CookingBox.Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.User
{
    [Route("api/v1/user/dishes")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class DishesUserController : Controller
    {
        private readonly IDishService _DishsService;
        private readonly IUriService _uriService;
        private readonly IMenuService _menuService;

        public DishesUserController(IDishService DishsService, IUriService uriService, IMenuService menuService)
        {
            _DishsService = DishsService;
            _uriService = uriService;
            _menuService = menuService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMenuDetailsUser([FromQuery] UserMenuListSearch userMenuListSearch)
        {
            var dishes = await _DishsService.GetDishesUser(userMenuListSearch);

            if (dishes != null && dishes.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<MenuDetail>.CreatePagedReponse(dishes, _uriService,
                           string.Concat(Request.Path.Value, Request.QueryString.Value)
                           );
                return Ok(pageResponse);
            }
            return NotFound();
        }

        [HttpGet("dish")]
        public async Task<IActionResult> GetDishUser([FromQuery] UserMenuListSearch userMenuListSearch)
        {
            var dish = await _DishsService.GetDishUser(userMenuListSearch);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpGet("dishtaste")]
        public async Task<IActionResult> GetDishUserByTaste([FromBody] UserMenuListSearch userMenuListSearch)
        {
            var dish = await _DishsService.GetDishByTaste(userMenuListSearch);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }


    }
}
