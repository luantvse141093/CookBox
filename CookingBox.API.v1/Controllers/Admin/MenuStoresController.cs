using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.API.v1.ResponseModels;
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
    
    public class MenuStoresController : Controller
    {
        private readonly IMenuStoreService _menuStoreService;
        private readonly IUriService _uriService;

        public MenuStoresController(IMenuStoreService menuStoreService, IUriService uriService)
        {
            _menuStoreService = menuStoreService;
            _uriService = uriService;

        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> InsertMenuStore([FromBody]MenuStoreViewModel menuStoreViewModel)
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
    }
}
