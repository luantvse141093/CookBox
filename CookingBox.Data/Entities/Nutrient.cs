using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Nutrient
    {
        public Nutrient()
        {
            NutrientDetails = new HashSet<NutrientDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
    }
}
