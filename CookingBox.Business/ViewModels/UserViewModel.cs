using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string? phone { get; set; }
        public string? role_id { get; set; }
        public string role_name { get; set; }
        public bool? status { get; set; }
        public string email { get; set; }
    }
}
