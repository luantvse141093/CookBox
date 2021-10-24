using System;
namespace CookingBox.Business.ViewModels
{
    public class NutrientDetailViewModel
    {
        public int id { get; set; }
        public int? nutrient_id { get; set; }
        public string nutrient_name { get; set; }
        public int? dish_id { get; set; }
        public double? amount { get; set; }
    }
}
