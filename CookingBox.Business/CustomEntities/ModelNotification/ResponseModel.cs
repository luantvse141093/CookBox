using System;
using Newtonsoft.Json;

namespace CookingBox.Business.CustomEntities.ModelNotification
{
    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
