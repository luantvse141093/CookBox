using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.User
{
    [Route("api/v1/user/categories")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class CategoriesUserController : Controller
    {
        private readonly ICategoryService _categoriesService;
        private readonly IUriService _uriService;

        public CategoriesUserController(ICategoryService categoriesService, IUriService uriService)
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
    }
}
