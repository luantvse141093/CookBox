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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/categories")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoriesService;
        private readonly IUriService _uriService;

        public CategoriesController(ICategoryService categoriesService, IUriService uriService)
        {
            _categoriesService = categoriesService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryListSearch categoryList)
        {
            var categories = await _categoriesService.GetCategories(categoryList);
            if (categories.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<CategoryViewModel>.CreatePagedReponse(categories, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                //var response = new ApiResponse<IEnumerable<DishDto>>(DishsDTO);
                return Ok(pageResponse);
            }
            return NotFound();

        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoriesService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoriesService.InsertCategory(categoryViewModel);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateDish(CategoryViewModel categoryViewModel)
        {

            var result = await _categoriesService.UpdateCategory(categoryViewModel);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoriesService.DeleteCategory(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
