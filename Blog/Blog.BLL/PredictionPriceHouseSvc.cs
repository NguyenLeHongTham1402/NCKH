using Blog.Common.BLL;
using Blog.Common.Req;
using Blog.Common.Rsp;
using Blog.DAL;
using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL
{
    public class PredictionPriceHouseSvc : GenericSvc<PredictionPriceHouseRep, UsaHousing>
    {
        PredictionPriceHouseRep rep = new PredictionPriceHouseRep();
        public SingleRsp PredictionPriceHouse(HouseDataPrediction h)
        {
            var res = new SingleRsp();
            var price = rep.PredictionPriceHouse(h);
            res.Data = price;
            return res;
        }
    }
}
