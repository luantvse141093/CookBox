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
    [Route("api/v1/admin/nutrients")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class NutrientsController : ControllerBase
    {
        private readonly INutrientService _nutrientService;
        private readonly IUriService _uriService;

        public NutrientsController(INutrientService nutrientService, IUriService uriService)
        {
            _nutrientService = nutrientService;
            _uriService = uriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTastes([FromQuery] NutrientListSearch nutrientListSearch)
        {
            var nutrients = await _nutrientService.GetNutrients(nutrientListSearch);
            if (nutrients.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<NutrientViewModel>.CreatePagedReponse(nutrients, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                return Ok(pageResponse);

            }
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNutrient(int id)
        {
            var metarial = await _nutrientService.GetNutrient(id);
            if (metarial != null)
            {

                return Ok(metarial);
            }
            return NotFound();
        }
    }
}
