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

        public long TotalRecords { get; }

        public IEnumerable<TEntity> Data { get; }

        public PagedResultSet(int pageIndex, int pageSize, int totalRecords, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
        }

    }
}