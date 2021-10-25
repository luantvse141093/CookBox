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
            MenuStores = new HashSet<MenuStore>();
        }

        public int Id { get; set; }
        public int? SessionId { get; set; }
        public bool? Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Session Session { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<MenuStore> MenuStores { get; set; }
    }
}
