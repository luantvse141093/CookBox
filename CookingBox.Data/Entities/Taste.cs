using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Taste
    {
        public Taste()
        {
            TasteDetails = new HashSet<TasteDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TasteDetail> TasteDetails { get; set; }
    }
}
