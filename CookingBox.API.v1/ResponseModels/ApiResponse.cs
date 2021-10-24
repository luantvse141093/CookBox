using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBox.API.v1.ResponseModels
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
