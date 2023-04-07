using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class UserReq
    {
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
        public string Token { get; set; }

        public UserReq()
        {
            Random r = new Random();
            int i = r.Next(2, 10);
            string path = string.Format("assets/av{0}.png", i);
            Avatar = path;
            CreatedDate = DateTime.Now;
            Role = "GUEST";
            IsActive = true;
        }
    }
}
