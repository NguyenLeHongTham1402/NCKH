using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class HouseDataPrediction
    {
        [ColumnName("AvgAreaIncome"), LoadColumn(0)]
        public float AvgAreaIncome { get; set; }

        [ColumnName("AvgAreaHouseAge"), LoadColumn(1)]
        public float AvgAreaHouseAge { get; set; }

        [ColumnName("AvgAreaNumberOfRooms"), LoadColumn(2)]
        public float AvgAreaNumberOfRooms { get; set; }

        [ColumnName("AvgAreaNumberOfBedrooms"), LoadColumn(3)]
        public float AvgAreaNumberOfBedrooms { get; set; }

        [ColumnName("AreaPopulation"), LoadColumn(4)]
        public float AreaPopulation { get; set; }

        [ColumnName("Price"), LoadColumn(5)]
        public float Price { get; set; }
    }
}
