using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public double? Total { get; set; }
        public DateTime? Date { get; set; }
        public string PaymentId { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public string OrderStatus { get; set; }
        public string Note { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Payment Payment { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Store Store { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
