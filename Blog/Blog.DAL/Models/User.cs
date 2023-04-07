using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.DAL.Models
{
    public partial class User
    {
        public User()
        {
            PostHouses = new HashSet<PostHouse>();
        }

        public int UserId { get; set; }
        public string ContactName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PostHouse> PostHouses { get; set; }
    }
}
