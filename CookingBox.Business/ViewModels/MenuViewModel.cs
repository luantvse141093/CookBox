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
        public string name { get; set; }
        public bool? status { get; set; }

        public virtual ICollection<MenuDetailViewModel> menu_details { get; set; }
        public virtual ICollection<MenuStoreViewModel> menu_stores { get; set; }



    }
}
