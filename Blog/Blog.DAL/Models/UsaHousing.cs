using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.DAL.Models
{
    public partial class UsaHousing
    {
        public double? AvgAreaIncome { get; set; }
        public double? AvgAreaHouseAge { get; set; }
        public double? AvgAreaNumberOfRooms { get; set; }
        public double? AvgAreaNumberOfBedrooms { get; set; }
        public double? AreaPopulation { get; set; }
        public double? Price { get; set; }
    }
}
