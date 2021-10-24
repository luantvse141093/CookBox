using CookingBox.Business.CustomEntities.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.IServices
{
    public interface IUriService
    {
        Uri GetPageUri(PagingParameters filter, string route);
    }
}
