using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class SearchPriceHouse
    {
        public decimal fromPrice { get; set; }
        public decimal toPrice { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public SearchPriceHouse()
        {
            Page = 1;
            Size = 5;
        }
    }
}
