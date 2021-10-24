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

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/stores")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUriService _uriService;

        public StoresController(IStoreService storeService, IUriService uriService)
        {
            _storeService = storeService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetStores([FromQuery] StoreListSearch storeListSearch)
        {
            var stores = await _storeService.GetStores(storeListSearch);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _storeService.GetStore(id);
            if (store != null)
            {
                //var response = new ApiResponse<StoreDto>(StoreDTO);
                return Ok(store);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> InsertStore([FromBody] StoreViewModel storeViewModel)
        {

            try
            {
                var storeId = await _storeService.InsertStore(storeViewModel);
                return Ok(storeId);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore(StoreViewModel storeViewModel)
        {

            var result = await _storeService.UpdateStore(storeViewModel);
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

            var result = await _storeService.DeleteStore(id);
            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }
    }
}
