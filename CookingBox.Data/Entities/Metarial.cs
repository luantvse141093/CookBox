using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Metarial
    {
        public Metarial()
        {
            DishIngredients = new HashSet<DishIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
