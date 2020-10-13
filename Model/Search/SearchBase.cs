using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Search
{
    public class SearchBase<T>
    {
        public string Token { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public T Data { get; set; }
    }
}
