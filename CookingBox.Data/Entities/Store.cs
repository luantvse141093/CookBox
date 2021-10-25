using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            MenuStores = new HashSet<MenuStore>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<MenuStore> MenuStores { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
