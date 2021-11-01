using CookingBox.Business.CustomEntities.SeedWork;
using System;
namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class MenuStoreListSearch : PagingParameters
    {
        public int? store_id { get; set; }
        public int? menu_id { get; set; }
    }
}
