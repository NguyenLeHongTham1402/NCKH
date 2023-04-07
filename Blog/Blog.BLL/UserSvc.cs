using Blog.Common.BLL;
using Blog.Common.Req;
using Blog.Common.Rsp;
using Blog.DAL;
using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Blog.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        UserRep rep = new UserRep();
        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.ContactName = userReq.ContactName;
            user.UserName = userReq.UserName;
            user.Password = userReq.Password;
            user.Email = userReq.Email;
            user.Address = userReq.Address;
            user.Role = userReq.Role;
            user.CreatedDate = userReq.CreatedDate;
            user.Avatar = userReq.Avatar;
            user.IsActive = userReq.IsActive;
            res = rep.CreateUser(user);
            return res;
        }

        public SingleRsp DeleteUser(int id)
        {
            var res = new SingleRsp();
            res = rep.DeleteUser(id);
            return res;
        }

        public SingleRsp LoginUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.UserName = userReq.UserName;
            user.Password = userReq.Password;
            res = rep.LoginUser(user);
            if (res.Success == true)
            {
                var u = (User)res.Data;
                UserReq req = new UserReq();
                req.UserId = u.UserId;
                req.UserName = u.UserName;
                req.Password = u.Password;
                req.ContactName = u.ContactName;
                req.Avatar = u.Avatar;
                req.Email = u.Email;
                req.Role = u.Role;
                req.Address = u.Address;
                req.CreatedDate = u.CreatedDate;
                req.IsActive = u.IsActive;
                res.Data = req;
                return res;
            }
            return res;
        }

        public SingleRsp UpdateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.ContactName = userReq.ContactName;
            user.UserName = userReq.UserName;
            user.Password = userReq.Password;
            user.Email = userReq.Email;
            user.Address = userReq.Address;
            user.Role = userReq.Role;
            user.CreatedDate = userReq.CreatedDate;
            user.Avatar = userReq.Avatar;
            user.IsActive = userReq.IsActive;
            res = rep.UpdateUser(user);
            return res;
        }

        public List<User> ListUsers()
        {
            return rep.ListUsers();
        }

        public UserReq FindUserByID(int id)
        {
            UserReq u = new UserReq();
            var p = rep.FindUserByID(id);
            u.ContactName = p.ContactName;
            u.UserName = p.UserName;
            u.Password = p.Password;
            u.Email = p.Email;
            u.Address = p.Address;
            u.Role = p.Role;
            u.CreatedDate = p.CreatedDate;
            u.Avatar = p.Avatar;
            u.IsActive = p.IsActive;
            return u;
        }

        public List<Report> ReportUser(ReportReq report)
        {
            return rep.ReportUser(report.month, report.year);
        }

    }
}
