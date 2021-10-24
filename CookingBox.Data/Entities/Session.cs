using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Session
    {
        public Session()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? TimeFrom { get; set; }
        public double? TimeTo { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
