using Blog.BLL;
using Blog.Common.Req;
using Blog.Common.Rsp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionPriceHouseController : ControllerBase
    {
        private readonly PredictionPriceHouseSvc prediction;
        public PredictionPriceHouseController()
        {
            prediction = new PredictionPriceHouseSvc();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, GUEST")]
        [HttpPost("Prediction")]
        public IActionResult PredictionPriceHouse([FromBody] HouseDataPrediction house)
        {
            var res = new SingleRsp();
            res.Data = prediction.PredictionPriceHouse(house);
            return Ok(res);
        }
    }
}
