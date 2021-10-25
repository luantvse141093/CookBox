using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string address { get; set; }

        [Phone]
        public string? phone { get; set; }

        public string? role_id { get; set; }
        public string role_name { get; set; }
        public bool? status { get; set; }

        [EmailAddress]
        public string email { get; set; }
    }
}
