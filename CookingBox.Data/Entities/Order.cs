using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual Payment Payment { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
