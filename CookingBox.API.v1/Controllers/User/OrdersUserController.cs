using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.Helppers;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.User
{
    [Route("api/v1/user/orders")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class OrdersUserController : ControllerBase
    {
        private readonly IOrderService _ordersService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IUriService _uriService;

        public OrdersUserController(IOrderService ordersService, IUriService uriService, IOrderDetailService orderDetailService)
        {
            _ordersService = ordersService;
            _uriService = uriService;
            _orderDetailService = orderDetailService;
        }
        // GET: api/<OrdersUserController>
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderListSearch orderListSearch)
        {
            var orders = await _ordersService.GetOrders(orderListSearch);
            if (orders.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<OrderViewModel>.CreatePagedReponse(orders, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                //var response = new ApiResponse<IEnumerable<DishDto>>(DishsDTO);
                return Ok(pageResponse);
            }
            return NotFound();
        }

        [HttpGet("orderDetail")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] OrderDetailListSearch orderDetailListSearch)
        {
            var orderDetails = await _orderDetailService.GetOrderDetails(orderDetailListSearch);
            if (orderDetails.Items.Count > 0)
            {
                var pageResponse = PaginationHelper<OrderDetailViewModel>.CreatePagedReponse(orderDetails, _uriService,
            string.Concat(Request.Path.Value, Request.QueryString.Value)
            );
                //var response = new ApiResponse<IEnumerable<DishDto>>(DishsDTO);
                return Ok(pageResponse);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder([FromBody] OrderViewModel OrderViewModel)
        {
            try
            {
                int id = await _ordersService.InsertOrder(OrderViewModel);
                if (id < 0)
                {
                    return BadRequest("Id Order Exist!");
                }
                return Ok(id);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder(int id, string note)
        {

            var result = await _ordersService.UpdateOrder(id, Business.Enums.OrderStatus.Cancelled.ToString(), note);

            if (result)
            {
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            return BadRequest();

        }


    }
}
