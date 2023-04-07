using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class PostHouseReq
    {
        public int PostHouseId { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public int? ViewCount { get; set; }

        public PostHouseReq()
        {
            CreatedDate = DateTime.Now;
            Status = "Draft";
            ViewCount = 0;
        }
    }
}
