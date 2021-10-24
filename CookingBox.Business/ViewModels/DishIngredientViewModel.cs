using System;
namespace CookingBox.Business.ViewModels
{
    public class DishIngredientViewModel
    {
        public int id { get; set; }
        public int? metarial_id { get; set; }
        public string metarial_name { get; set; }
        public double? quantity { get; set; }
        public int dish_id { get; set; }
    }
}
