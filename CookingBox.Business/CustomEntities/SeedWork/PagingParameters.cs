using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.CustomEntities.SeedWork
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        public int page_number { get; set; } = 1;
        private int _pageSize = 10;
        public int page_size
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
