using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Session
    {
        public Session()
        {
            MenuStores = new HashSet<MenuStore>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? TimeFrom { get; set; }
        public double? TimeTo { get; set; }

        public virtual ICollection<MenuStore> MenuStores { get; set; }
    }
}
