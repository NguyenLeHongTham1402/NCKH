using Blog.Common.DAL;
using Blog.Common.Req;
using Blog.DAL.Models;
using Blog.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL
{
    public class PredictionPriceHouseRep : GenericRep<BlogContext, UsaHousing>
    {
        public float PredictionPriceHouse(HouseDataPrediction house)
        {
            var h = new HousingData();
            var p = new PredictionPrice();
            h.AvgAreaIncome = house.AvgAreaIncome;
            h.AvgAreaNumberOfRooms = house.AvgAreaNumberOfRooms;
            h.AvgAreaNumberOfBedrooms = house.AvgAreaNumberOfBedrooms;
            h.AreaPopulation = house.AreaPopulation;
            float kq = p.PredictionPriceHose(h);
            return kq;
        }
    }
}
