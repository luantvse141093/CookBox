using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Dishes = new HashSet<Dish>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
