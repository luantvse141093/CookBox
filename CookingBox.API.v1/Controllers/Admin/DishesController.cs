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
using Microsoft.AspNetCore.Authorization;

namespace CookingBox.Api.Controllers.Admin
{
    [Route("api/v1/admin/dishes")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _DishsService;
        private readonly IUriService _uriService;

        public DishesController(IDishService DishsService, IUriService uriService)
        {
            _DishsService = DishsService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetDishes([FromQuery] DishListSearch dishListSearch)
        {
            var dishes = await _DishsService.GetDishes(dishListSearch);
            if (dishes.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<DishViewModel>.CreatePagedReponse(dishes, _uriService,
                           string.Concat(Request.Path.Value, Request.QueryString.Value)
                           );
                return Ok(pageResponse);

            }
            return NotFound();

            //var response = new ApiResponse<IEnumerable<DishDto>>(DishsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDish(int id)
        {
            var dish = await _DishsService.GetDish(id);
            if (dish == null)
            {
                return NotFound();
            }
            //var response = new ApiResponse<DishDto>(DishDTO);
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDish([FromBody] DishViewModel dishViewModel)
        {
            try
            {
                await _DishsService.InsertDish(dishViewModel);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDish(DishViewModel dishViewModel)
        {
            var result = await _DishsService.UpdateDish(dishViewModel);
            var response = new ApiResponse<bool>(result);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _DishsService.DeleteDish(id);
            if (!result)
            {
                return BadRequest();
            }
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
