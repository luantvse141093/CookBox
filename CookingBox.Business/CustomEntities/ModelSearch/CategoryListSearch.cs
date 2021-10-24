using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.Enums;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class CategoryListSearch : PagingParameters
    {
        public string? name { get; set; }
        public Sort? sort { get; set; }


    }
}   
