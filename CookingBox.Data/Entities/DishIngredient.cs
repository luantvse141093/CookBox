using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class DishIngredient
    {
        public int Id { get; set; }
        public int? MetarialId { get; set; }
        public double? Quantity { get; set; }
        public int? DishId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dish Dish { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Metarial Metarial { get; set; }
    }
}
