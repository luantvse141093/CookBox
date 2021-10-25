using CookingBox.Business.Enums;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.ViewModels
{
    public class OrderViewModel
    {
        public int id { get; set; }
        public DateTime? date { get; set; }
        public string payment_name { get; set; }
        public string payment_id { get; set; }
        public int? user_id { get; set; }
        public int? store_id { get; set; }
        public string? user_name { get; set; }
        public string? store_name { get; set; }
        public double? total { get; set; }
        public string? note { get; set; }

        public OrderStatus order_status { get; set; }

        public List<OrderDetailViewModel> orderDetails { get; set; }



    }
}
