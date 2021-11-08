using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Dish
    {
        public Dish()
        {
            DishIngredients = new HashSet<DishIngredient>();
            MenuDetails = new HashSet<MenuDetail>();
            NutrientDetails = new HashSet<NutrientDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            Repices = new HashSet<Repice>();
            TasteDetails = new HashSet<TasteDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Meal { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }
        public int? ParentId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Category Category { get; set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Repice> Repices { get; set; }
        public virtual ICollection<TasteDetail> TasteDetails { get; set; }
    }
}
