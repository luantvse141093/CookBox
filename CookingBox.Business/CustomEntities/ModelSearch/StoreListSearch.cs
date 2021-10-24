using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.SeedWork;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class StoreListSearch : PagingParameters
    {
        public string? name { get; set; }

    }
}
