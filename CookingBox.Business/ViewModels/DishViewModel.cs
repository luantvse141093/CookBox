using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Business.ViewModels
{
    public class DishViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int meal { get; set; }
        public string image { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public bool status { get; set; }
        public int parent_id { get; set; } = 0;

        public virtual ICollection<DishIngredientViewModel> dish_ingredients { get; set; }
        public virtual ICollection<NutrientDetailViewModel> nutrient_details { get; set; }
        public virtual ICollection<Repice> repices { get; set; }
        public virtual ICollection<TasteDetailViewModel> taste_details { get; set; }



    }
}
