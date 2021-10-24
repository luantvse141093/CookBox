using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.CustomEntities.SeedWork
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public Uri PreviousPage { get; set; }
        public Uri CurrentPageUri { get; set; }
        public Uri NextPage { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
    }
}
