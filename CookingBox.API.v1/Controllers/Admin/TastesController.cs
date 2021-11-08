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
    [Route("api/v1/admin/tastes")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class TastesController : ControllerBase
    {
        private readonly ITasteService _tasteService;
        private readonly IUriService _uriService;

        public TastesController(ITasteService tasteService, IUriService uriService)
        {
            _tasteService = tasteService;
            _uriService = uriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTastes([FromQuery] TasteListSearch tasteListSearch)
        {
            var tates = await _tasteService.GetTastes(tasteListSearch);
            if (tates.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<TasteViewModel>.CreatePagedReponse(tates, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                return Ok(pageResponse);

            }
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaste(int id)
        {
            var metarial = await _tasteService.GetTaste(id);
            if (metarial != null)
            {

                return Ok(metarial);
            }
            return NotFound();
        }
    }
}
