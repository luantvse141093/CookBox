using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class MenuDetail
    {
        public int Id { get; set; }
        public int? DishId { get; set; }
        public int? MenuId { get; set; }
        public double? Price { get; set; }

        public virtual Dish Dish { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Menu Menu { get; set; }
    }
}
