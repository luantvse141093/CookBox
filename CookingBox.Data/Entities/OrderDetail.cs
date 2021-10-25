using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? DishId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dish Dish { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
