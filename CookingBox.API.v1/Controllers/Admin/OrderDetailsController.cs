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
    [Route("api/v1/admin/orderdetails")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _OrderDetailsService;
        private readonly IUriService _uriService;
        public OrderDetailsController(IOrderDetailService orderDetailsService, IUriService uriService)
        {
            _OrderDetailsService = orderDetailsService;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetail([FromQuery] OrderDetailListSearch orderDetailListSearch)
        {
            var orderDetails = await _OrderDetailsService.GetOrderDetails(orderDetailListSearch);
            if (orderDetails == null)
            {
                return NotFound();
            }
            var pageResponse = PaginationHelper<OrderDetailViewModel>.CreatePagedReponse(orderDetails, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
           );
            return Ok(pageResponse);
        }
    }
}
