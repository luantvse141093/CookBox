using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.SeedWork;

namespace CookingBox.Business.CustomEntities.ModelSearch
{
    public class UserListSearch : PagingParameters
    {
        public string? email { get; set; }
        public string? role_id { get; set; }

        public bool? status { get; set; }
    }
}
