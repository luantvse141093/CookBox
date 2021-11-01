﻿using CookingBox.API.v1.ResponseModels;
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

namespace CookingBox.Api.Controllers.Admin
{
    [Route("api/v1/admin/orders")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _ordersService;
        private readonly IUriService _uriService;

        public OrdersController(IOrderService ordersService, IUriService uriService)
        {
            _ordersService = ordersService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderListSearch orderListSearch)
        {
            var Orders = await _ordersService.GetOrders(orderListSearch);
            var pageResponse = PaginationHelper<OrderViewModel>.CreatePagedReponse(Orders, _uriService,
             string.Concat(Request.Path.Value, Request.QueryString.Value)
             );
            //var response = new ApiResponse<IEnumerable<OrderDto>>(OrdersDTO);
            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var Order = await _ordersService.GetOrder(id);
            //var response = new ApiResponse<OrderDto>(OrderDTO);
            return Ok(Order);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder([FromBody] OrderViewModel orderViewModel)
        {

            await _ordersService.InsertOrder(orderViewModel);
            return Ok();
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder(int id, string note)
        {
            String mode = "processing";

            var result = await _ordersService.UpdateOrder(id, Business.Enums.OrderStatus.Cancelled.ToString(), note, mode);

            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            String mode = "processing";
            var result = await _ordersService.UpdateOrder(id, Business.Enums.OrderStatus.Processing.ToString(), "", mode);

            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();
        }


    }
}
