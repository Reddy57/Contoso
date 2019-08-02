using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso.API.Utilities
{
    public class PagedResultSet<TEntity> where TEntity : class
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public long Count { get; }

        public IEnumerable<TEntity> Data { get; }

        public PagedResultSet(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

    }
}