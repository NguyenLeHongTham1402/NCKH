using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.DAL.Models
{
    public partial class PostHouse
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

        public virtual User User { get; set; }
    }
}
