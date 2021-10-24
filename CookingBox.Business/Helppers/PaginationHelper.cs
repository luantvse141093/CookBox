using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.Helppers
{
    public class PaginationHelper<T>
    {
        public static PagedList<T> CreatePagedReponse(PagedList<T> pagedData, IUriService _uriService, string route)
        {
            var metaData = pagedData.MetaData;
            metaData.CurrentPageUri =
               metaData.CurrentPage >= 1 && metaData.CurrentPage <= metaData.TotalPages
               ? _uriService.GetPageUri(new PagingParameters
               {
                   page_number = metaData.CurrentPage,
                   page_size = metaData.PageSize
               }
               , route) : null;


            metaData.NextPage =
              metaData.CurrentPage >= 1 && metaData.CurrentPage < metaData.TotalPages
              ? _uriService.GetPageUri(new PagingParameters
              {
                  page_number = metaData.CurrentPage + 1,
                  page_size = metaData.PageSize
              }
              , route) : null;

            metaData.PreviousPage =
                metaData.CurrentPage - 1 >= 1 && metaData.CurrentPage <= metaData.TotalPages
                ? _uriService.GetPageUri(new PagingParameters
                {
                    page_number = metaData.CurrentPage - 1,
                    page_size = metaData.PageSize
                }
                , route) : null;

            metaData.FirstPage =
                 metaData.TotalPages > 0
                ? _uriService.GetPageUri(
                new PagingParameters
                { page_number = 1, page_size = metaData.PageSize }
                , route) : null;

            metaData.LastPage =
                metaData.TotalPages > 0
                ? _uriService.GetPageUri(
                new PagingParameters
                { page_number = metaData.TotalPages, page_size = metaData.PageSize }
                , route) : null;
            return pagedData;
        }
    }
}
