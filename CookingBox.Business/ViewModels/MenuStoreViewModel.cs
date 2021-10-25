using System;
namespace CookingBox.Business.ViewModels
{
    public class MenuStoreViewModel
    {
        public int id { get; set; }
        public int? menu_id { get; set; }
        public int? store_id { get; set; }
        public string? store_name { get; set; }
        public bool? status { get; set; }
    }
}
