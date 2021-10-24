using System;
using CookingBox.Business.CustomEntities.SeedWork;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class OrderDetailListSearch : PagingParameters
    {
        public int? order_id { get; set; }
    }
}
