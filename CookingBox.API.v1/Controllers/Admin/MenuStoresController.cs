using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/menustores")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class MenuStoresController : ControllerBase
    {
        private readonly IMenuStoreService _menuStoreService;
        private readonly IUriService _uriService;

        public MenuStoresController(IMenuStoreService menuStoreService, IUriService uriService)
        {
            _menuStoreService = menuStoreService;
            _uriService = uriService;

        }
        [HttpGet]
        public async Task<IActionResult> GetMenuStores([FromQuery] MenuStoreListSearch menuStoreListSearch)
        {
            var menuStores = await _menuStoreService.GetMenuStores(menuStoreListSearch);
            var pageResponse = PaginationHelper<MenuStoreViewModel>.CreatePagedReponse(menuStores, _uriService,
             string.Concat(Request.Path.Value, Request.QueryString.Value)
             );
            //var response = new ApiResponse<IEnumerable<PaymentDto>>(PaymentsDTO);
            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuStore(int id)
        {
            var menuStore = await _menuStoreService.GetMenuStore(id);
            if (menuStore != null)
            {
                return Ok(menuStore);
            }
            return NotFound();

        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> InsertMenuStore([FromBody] MenuStoreViewModel menuStoreViewModel)
        {
            var result = await _menuStoreService.InsertMenuStore(menuStoreViewModel);
            if (result > 0)
            {
                var response = new ApiResponse<int>(result);
                return Ok(response);
            }
            return BadRequest();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuStore(int id)
        {
            var result = await _menuStoreService.DeleteMenuStore(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuStore(MenuStoreViewModel menuStoreViewModel)
        {

            var result = await _menuStoreService.UpdateMenuStore(menuStoreViewModel);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
