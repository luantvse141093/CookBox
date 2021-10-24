using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class MenuListSearch : PagingParameters
    {
        public string? name { get; set; }
        public Sort? sort_name { get; set; }
        public int? store_id { get; set; }
    }
}
