using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;

namespace CookingBox.Business.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PagingParameters filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));

            var baseUri = _enpointUri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
            var query = QueryHelpers.ParseQuery(_enpointUri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            items.RemoveAll(x => x.Key == "page_number");
            items.RemoveAll(x => x.Key == "page_size");
            var qb = new QueryBuilder(items);
            qb.Add("page_number", filter.page_number.ToString());
            qb.Add("page_size", filter.page_size.ToString());

            var fullUri = baseUri + qb.ToQueryString();
            //return new Uri(modifiedUri);
            return new Uri(fullUri);
        }
    }
}