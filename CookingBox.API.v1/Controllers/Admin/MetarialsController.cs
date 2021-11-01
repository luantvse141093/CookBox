using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/v1/admin/metarials")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class MetarialsController : ControllerBase
    {

        private readonly IMetarialService _metarialService;
        private readonly IUriService _uriService;

        public MetarialsController(IMetarialService metarialService, IUriService uriService)
        {
            _metarialService = metarialService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMetarials([FromQuery] MetarialListSearch metarialListSearch)
        {
            var metarials = await _metarialService.GetMetarials(metarialListSearch);
            if (metarials.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<MetarialViewModel>.CreatePagedReponse(metarials, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                return Ok(pageResponse);

            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMetarial(int id)
        {
            var metarial = await _metarialService.GetMetarial(id);
            if (metarial != null)
            {

                return Ok(metarial);
            }
            return NotFound();
        }


    }
}
