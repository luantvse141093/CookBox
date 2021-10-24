using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class StoreViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public bool? status { get; set; }


        
    }
}
