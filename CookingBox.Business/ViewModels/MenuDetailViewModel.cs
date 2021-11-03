using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;

namespace CookingBox.Business.ViewModels
{
    public class MenuDetailViewModel
    {
        public int id { get; set; }
        public int? dish_id { get; set; }
        public string? dish_name { get; set; }
        public string? dish_image { get; set; }
        public double? price { get; set; }
        public bool status { get; set; }


    }
}
