using System;
namespace CookingBox.Business.ViewModels
{
    public class OrderDetailViewModel
    {
        public int id { get; set; }
        public int? order_id { get; set; }
        public int? dish_id { get; set; }
        public string dish_name { get; set; }
        public double? price { get; set; }
        public int? quantity { get; set; }
    }
}
