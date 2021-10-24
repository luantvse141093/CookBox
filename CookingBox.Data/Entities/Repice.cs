using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Repice
    {
        public Repice()
        {
            Steps = new HashSet<Step>();
        }

        public int Id { get; set; }
        public int? DishId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dish Dish { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
