using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class SearchHouseKeyword
    {
        public string Keyword { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public SearchHouseKeyword()
        {
            Page = 1;
            Size = 5;
        }
    }
}
