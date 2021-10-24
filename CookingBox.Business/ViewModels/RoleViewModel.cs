using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class RoleViewModel
    {
        public string id { get; set; }
        public string role_name { get; set; }
        public bool? status { get; set; }

    }
}
