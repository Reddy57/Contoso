using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Utility
{
    public class PagedResultSet<T> where T : class
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public long Count { get; }

        public IEnumerable<T> Data { get; }

        public PagedResultSet(int pageIndex, int pageSize, int count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

    }
}
