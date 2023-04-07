using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ML
{
    public class HousingPrediction
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }
}
