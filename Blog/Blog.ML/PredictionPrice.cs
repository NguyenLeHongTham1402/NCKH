using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Blog.ML
{
    public class PredictionPrice
    {
        public float PredictionPriceHose(HousingData h)
        {
            var connectionString = @"Data Source=LAPTOP-82EJ2NL2\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True";
            var loadColunms = new DatabaseLoader.Column[]
            {
                new DatabaseLoader.Column(){Name="AvgAreaIncome", Type=DbType.Single},
                new DatabaseLoader.Column(){Name="AvgAreaHouseAge", Type=DbType.Single},
                new DatabaseLoader.Column(){Name="AvgAreaNumberOfRooms", Type=DbType.Single},
                new DatabaseLoader.Column(){Name="AvgAreaNumberOfBedrooms", Type=DbType.Single},
                new DatabaseLoader.Column(){Name="AreaPopulation", Type=DbType.Single},
                new DatabaseLoader.Column(){Name="Price", Type=DbType.Single}
            };

            var connection = new SqlConnection(connectionString);
            var factory = DbProviderFactories.GetFactory(connection);
            var context = new MLContext();
            var loader = context.Data.CreateDatabaseLoader(loadColunms);
            var dbsource = new DatabaseSource(factory, connectionString, "select CAST([AvgAreaIncome] as REAL) as [AvgAreaIncome]," +
                "CAST([AvgAreaHouseAge] as REAL) as [AvgAreaHouseAge],CAST([AvgAreaNumberOfRooms] as REAL) as [AvgAreaNumberOfRooms]," +
                "CAST([AvgAreaNumberOfBedrooms] as REAL) as [AvgAreaNumberOfBedrooms]" +
                ",CAST([AreaPopulation] as REAL) as [AreaPopulation], CAST([Price] as REAL) as [Price] from [dbo].[UsaHousing]");
            var data = loader.Load(dbsource);
            //var preview = data.Preview();
            TrainTestData trainTestData = context.Data.TrainTestSplit(data, testFraction: 0.2);
            var pipeline = context.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Price")
            .Append(context.Transforms.Concatenate("Features", "AvgAreaIncome", "AvgAreaHouseAge", "AvgAreaNumberOfRooms", "AvgAreaNumberOfBedrooms", "AreaPopulation"))
            .Append(context.Regression.Trainers.FastTree());
            var model = pipeline.Fit(trainTestData.TrainSet);
            var predictions = model.Transform(data);
            var metrics = context.Regression.Evaluate(predictions, "Label", "Score");
            var output = context.Model.CreatePredictionEngine<HousingData, HousingPrediction>(model).Predict(h);
            return output.Price;
        }
    }
}
