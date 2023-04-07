using Blog.Common.DAL;
using Blog.Common.Rsp;
using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Blog.DAL
{
    public class PostHouseRep : GenericRep<BlogContext, PostHouse>
    {
        public SingleRsp CreateHouse(PostHouse house)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.PostHouses.Add(house);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Create Post Success!!!");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp UpdateHouse(PostHouse house)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.PostHouses.Update(house);
                        context.SaveChanges();
                        tran.Commit();
                        res.SetMessage("Update Post Success!!!");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp DeleteHouse(int id)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.PostHouses.SingleOrDefault(x => x.PostHouseId == id);
                        if (p != null)
                        {
                            Context.PostHouses.Remove(p);
                            Context.SaveChanges();
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public List<PostHouse> SearchHousePrice(decimal fromPrice, decimal toPrice)
        {
            return All.Where(p => p.Price >= fromPrice && p.Price <= toPrice).ToList();
        }

        public List<PostHouse> SearchKeyword(string keyword)
        {
            return All.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword)).ToList();
        }

        public List<PostHouse> ListHouses()
        {
            var p = All.Select(p => p).ToList();
            return p;
        }

        public List<PostHouse> MyListHouses(int id)
        {
            var p = All.Where(p=>p.UserId==id).ToList();
            return p;
        }

        public PostHouse DetailPostHouse(int id)
        {
            var p = All.FirstOrDefault(p => p.PostHouseId == id);
            return p;
        }

        public List<Report> ReportHousing(int month, int year)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-82EJ2NL2\\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True");
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand("spThongKeBaiDang", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@month", SqlDbType.Int).Value = month;
            com.Parameters.Add("@year", SqlDbType.Int).Value = year;
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            List<Report> reports = new List<Report>();
            foreach (DataRow dr in dt.Rows)
            {
                Report r = new Report();
                r.year = int.Parse(dr[0].ToString());
                r.month = int.Parse(dr[1].ToString());
                r.count = int.Parse(dr[2].ToString());
                reports.Add(r);
            }
            return reports;
        }
    }
}
