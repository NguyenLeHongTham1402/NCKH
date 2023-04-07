using Blog.Common.BLL;
using Blog.Common.Req;
using Blog.Common.Rsp;
using Blog.DAL;
using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.BLL
{
    public class PostHouseSvc : GenericSvc<PostHouseRep, PostHouse>
    {
        PostHouseRep rep = new PostHouseRep();

        public SingleRsp CreateHouse(PostHouseReq houseReq)
        {
            var res = new SingleRsp();
            PostHouse house = new PostHouse();
            house.Title = houseReq.Title;
            house.Price = houseReq.Price;
            house.Address = houseReq.Address;
            house.Description = houseReq.Description;
            house.Phone = houseReq.Phone;
            house.CreatedDate = houseReq.CreatedDate;
            house.UserId = houseReq.UserId;
            house.UserName = houseReq.UserName;
            house.Status = houseReq.Status;
            house.ViewCount = houseReq.ViewCount;
            res = rep.CreateHouse(house);
            return res;
        }

        public SingleRsp UpdateHouse(PostHouseReq houseReq)
        {
            var res = new SingleRsp();
            PostHouse house = new PostHouse();
            house.PostHouseId = houseReq.PostHouseId;
            house.Title = houseReq.Title;
            house.Price = houseReq.Price;
            house.Address = houseReq.Address;
            house.Description = houseReq.Description;
            house.Phone = houseReq.Phone;
            house.CreatedDate = houseReq.CreatedDate;
            house.UserId = houseReq.UserId;
            house.UserName = houseReq.UserName;
            house.Status = houseReq.Status;
            house.ViewCount = houseReq.ViewCount;
            res = rep.UpdateHouse(house);
            return res;
        }

        public SingleRsp DeleteHouse(int id)
        {
            var res = new SingleRsp();
            res = rep.DeleteHouse(id);
            return res;
        }

        public SingleRsp SearchHousePrice(SearchPriceHouse s)
        {
            var res = new SingleRsp();
            //Lấy DS House
            var houses = rep.SearchHousePrice(s.fromPrice, s.toPrice);

            int hCount, totalPages, offset;
            offset = s.Size * (s.Page - 1);
            hCount = houses.Count;
            totalPages = (hCount % s.Size) == 0 ? hCount / s.Size : 1 + (hCount / s.Size);
            var o = new
            {
                Data = houses.Skip(offset).Take(s.Size).ToList(),
                Page = s.Page,
                Size = s.Size,
                TotalPages = totalPages
            };
            res.Data = o;
            return res;
        }

        public SingleRsp SearchHouseKeyword(SearchHouseKeyword k)
        {
            var res = new SingleRsp();
            //Lấy DS House
            var houses = rep.SearchKeyword(k.Keyword);

            int hCount, totalPages, offset;
            offset = k.Size * (k.Page - 1);
            hCount = houses.Count;
            totalPages = (hCount % k.Size) == 0 ? hCount / k.Size : 1 + (hCount / k.Size);
            var o = new
            {
                Data = houses.Skip(offset).Take(k.Size).ToList(),
                Page = k.Page,
                Size = k.Size,
                TotalPages = totalPages
            };
            res.Data = o;
            return res;
        }
        public List<PostHouse> ListHouse()
        {
            return rep.ListHouses();
        }

        public List<PostHouse> MyListHouse(int id)
        {
            return rep.MyListHouses(id);
        }

        public PostHouse DetailPostHouse(int id)
        {
            return rep.DetailPostHouse(id);
        }

        public List<Report> ReportHousing(ReportReq report)
        {
            return rep.ReportHousing(report.month, report.year);
        }

    }
}
