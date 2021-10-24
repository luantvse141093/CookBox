using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            Menus = new HashSet<Menu>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
