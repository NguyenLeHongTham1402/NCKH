using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Req
{
    public class PredictionPriceHouseOut
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }
}
