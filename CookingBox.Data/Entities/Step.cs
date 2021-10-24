using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Step
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? RepiceId { get; set; }
        public string Image { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Repice Repice { get; set; }
    }
}
