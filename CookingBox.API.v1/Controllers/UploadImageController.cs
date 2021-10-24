using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CookingBox.API.v1.Controllers
{
    [Route("api/v1/image")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private static string ApiKey = "AIzaSyD_goQP09RrTFNNmJH1SXX2vO1VDCLPrJs";
        private static string Bucket = "cookingbox-project.appspot.com";
        private static string AuthEmail = "nguyenvanluan1789@gmail.com";
        private static string AuthPassword = "1421Luan";
        private readonly IDistributedCache _distributedCache;


        public UploadImageController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {

            string cachedImageUrlString = string.Empty;
            cachedImageUrlString = await _distributedCache.GetStringAsync("_imageUrl");

            if (file.Length > 0)
            {
                var ms = file.OpenReadStream();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);


                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("assets")
                    .Child("image")
                      .Child($"{file.FileName}")

                      .PutAsync(ms, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
                await _distributedCache.SetStringAsync("_imageUrl", (await task).ToString(), expiryOptions);

                return Ok((await task).ToString());


            }
            return BadRequest();

        }



    }
}