using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingBox.API.v1.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CBPlicoy")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private static string ApiKey = "AIzaSyD_goQP09RrTFNNmJH1SXX2vO1VDCLPrJs";
        private static string Bucket = "cookingbox-project.appspot.com";
        private static string AuthEmail = "nguyenvanluan1789@gmail.com";
        private static string AuthPassword = "1421Luan";


        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {

            if (file.Length > 0)
            {
                var ms = file.OpenReadStream();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child("assets")
                    .Child("image")
                      .Child($"{file.FileName}")
                      //.Child($"{file.FileName}.{Path.GetExtension(file.FileName).Substring(1)}")
                      .PutAsync(ms, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                return Ok((await task).ToString());


            }
            return BadRequest();

        }



    }
}
