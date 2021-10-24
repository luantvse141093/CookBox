using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.CustomEntities.ModelSearch.User
{
    public class UserMenuListSearch : PagingParameters
    {
        public int store_id { get; set; } = 0;
        public string? name { get; set; }
        public int? category_id { get; set; }
        public int dish_id { get; set; } = 0;

        public List<TasteDetail> list_taste { get; set; }
    }

}
