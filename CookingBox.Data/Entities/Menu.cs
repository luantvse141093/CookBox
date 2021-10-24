using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int? SessionId { get; set; }
        public bool? Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Session Session { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
