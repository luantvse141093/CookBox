using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class OrderListSearch : PagingParameters
    {
        public DateTime? date { get; set; }

        public Sort? sort_date { get; set; }
        public int? user_id { get; set; }
        public int? store_id { get; set; }

        public OrderStatus? order_status { get; set; }
    }
}
