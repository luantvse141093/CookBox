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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.Admin
{
    [Route("api/v1/admin/payments")]
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUriService _uriService;

        public PaymentsController(IPaymentService paymentService, IUriService uriService)
        {
            _paymentService = paymentService;
            _uriService = uriService;

        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentes([FromQuery] PaymentListSearch paymentListSearch)
        {
            var paymentes = await _paymentService.GetPayments(paymentListSearch);
            var pageResponse = PaginationHelper<PaymentViewModel>.CreatePagedReponse(paymentes, _uriService,
             string.Concat(Request.Path.Value, Request.QueryString.Value)
             );
            //var response = new ApiResponse<IEnumerable<PaymentDto>>(PaymentsDTO);
            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(string id)
        {
            var payment = await _paymentService.GetPayment(id);
            //var response = new ApiResponse<PaymentDto>(PaymentDTO);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPayment([FromBody] PaymentViewModel paymentViewModel)
        {

            await _paymentService.InsertPayment(paymentViewModel);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment(PaymentViewModel paymentViewModel)
        {

            var result = await _paymentService.UpdatePayment(paymentViewModel);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _paymentService.DeletePayment(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
