using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelNotification;
using CookingBox.Business.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingBox.API.v1.Controllers
{
    [Route("api/v1/notification")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [HttpGet("aa")]
        public async Task<IActionResult> SendNotification111(string aa)
        {
            
            return Ok(BCrypt.Net.BCrypt.Verify("123456", "$2a$11$.pVgUdyUjDJt8VbVxl3jCeV471/NKREHqU6RuApRFbOCd3gPi/tKK"));
        }
    }
}
