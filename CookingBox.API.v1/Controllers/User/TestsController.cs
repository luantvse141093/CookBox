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
    [Route("api/v1/tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        List<string> todos = new List<string> { "luan", "luan123" };
        private readonly IDistributedCache _distributedCache;
        public TestsController(IDistributedCache distributedCach)
        {
            _distributedCache = distributedCach;
        }
        //GET: api/<TestsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<string> myTodos = new List<string>();
            bool IsCached = false;
            string cachedTodosString = string.Empty;
            cachedTodosString = await _distributedCache.GetStringAsync("_todos");
            if (!string.IsNullOrEmpty(cachedTodosString))
            {

                myTodos = JsonSerializer.Deserialize<List<string>>(cachedTodosString);
                IsCached = true;
            }
            else
            {

                myTodos = todos;

                IsCached = true;
                cachedTodosString = JsonSerializer.Serialize<List<string>>(todos);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };
                await _distributedCache.SetStringAsync("_todos", cachedTodosString, expiryOptions);
            }
            return Ok(new { IsCached, myTodos });
        }

        [HttpGet("clear")]
        public async Task<IActionResult> ClearCache(string key)
        {
            await _distributedCache.RemoveAsync(key);
            return Ok(new { Message = $"cleared cache for key -{key}" });
        }


    }
}
