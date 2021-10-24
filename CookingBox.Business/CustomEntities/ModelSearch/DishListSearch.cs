using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.Enums;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class DishListSearch : PagingParameters
    {
        public string? name { get; set; }

        public int? category_id { get; set; }

        public bool? status { get; set; }

        public Sort? sort_name { get; set; }

        public int? taste_id { get; set; }





    }
}
