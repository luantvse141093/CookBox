using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBox.API.v1.Controllers.User
{
    [Authorize(Policy = "US")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoresUserController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUriService _uriService;

        public StoresUserController(IStoreService storeService, IUriService uriService)
        {
            _storeService = storeService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetStoresUser([FromQuery] StoreListSearch storeListSearch)
        {
            var stores = await _storeService.GetStoresUser(storeListSearch);
            if (stores.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<StoreViewModel>.CreatePagedReponse(stores, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                //var response = new ApiResponse<IEnumerable<StoreDto>>(StoresDTO);
                return Ok(pageResponse);
            }
            return NotFound();

        }
    }
}
