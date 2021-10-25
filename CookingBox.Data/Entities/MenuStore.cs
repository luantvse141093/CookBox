using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class MenuStore
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? StoreId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Menu Menu { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Store Store { get; set; }
    }
}
