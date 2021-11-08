using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class TasteDetail
    {
        public int Id { get; set; }
        public int? TasteId { get; set; }
        public int? TasteLevel { get; set; }
        public int? DishId { get; set; }
        public bool? Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dish Dish { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Taste Taste { get; set; }
    }
}
