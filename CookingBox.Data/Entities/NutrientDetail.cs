using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class NutrientDetail
    {
        public int Id { get; set; }
        public int? NutrientId { get; set; }
        public int? DishId { get; set; }
        public double? Amount { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dish Dish { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Nutrient Nutrient { get; set; }
    }
}
