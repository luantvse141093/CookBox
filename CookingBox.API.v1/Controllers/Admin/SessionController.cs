using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.API.v1.ResponseModels;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.Admin
{
    [Authorize(Policy = "AD")]
    [EnableCors("CBPolicy")]
    [Route("api/v1/admin/sessions")]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSessions(SessionListSearch sessionListSearch)
        {
            try
            {
                //var response = new ApiResponse<bool>(_sessionService.GetSessions(sessionListSearch));
                return Ok(await _sessionService.GetSessions(sessionListSearch));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(int id)
        {
                try
                {
                    //var response = new ApiResponse<bool>(_sessionService.GetSessions(sessionListSearch));
                    return Ok(await _sessionService.GetSession(id));
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

        
    }
}
