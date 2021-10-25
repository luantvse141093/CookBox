using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class MenuViewModel
    {
        public int id { get; set; }
        public int session_id { get; set; }
        public string session_name { get; set; }
        public double? TimeFrom { get; set; }
        public double? TimeTo { get; set; }
        public bool? status { get; set; }

        public virtual ICollection<MenuDetailViewModel> menu_details { get; set; }


    }
}
