using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool? Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
