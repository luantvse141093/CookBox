using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers.User
{
    //[Authorize(Policy = "US")]
    [Route("api/v1/redis")]
    [ApiController]
    public class CacheRedisController : ControllerBase
    {
        
        private readonly IDistributedCache _distributedCache;
        public CacheRedisController(IDistributedCache distributedCach)
        {
            _distributedCache = distributedCach;
        }
        //GET: api/<TestsController>
        [HttpGet("{cache}")]
        public async Task<IActionResult> GetCache(string cache)
        {
            //List<string> myTodos = new List<string>();
            //bool IsCached = false;
            string cachedString = string.Empty;
            cachedString = await _distributedCache.GetStringAsync(cache);
            if (!string.IsNullOrEmpty(cachedString))
            {
                return Ok(cachedString);
            }
           
            return BadRequest();
        }

        [HttpGet("clear")]
        public async Task<IActionResult> ClearCache(string key)
        {
            await _distributedCache.RemoveAsync(key);
            return Ok(new { Message = $"cleared cache for key -{key}" });
        }


    }
}
