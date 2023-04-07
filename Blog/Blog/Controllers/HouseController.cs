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
    public class HouseController : ControllerBase
    {
        private readonly PostHouseSvc postHouseSvc;
        public HouseController()
        {
            postHouseSvc = new PostHouseSvc();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, GUEST")]
        [HttpPost("create-house")]
        public IActionResult CreateHouse([FromBody] PostHouseReq houseReq)
        {
            var res = postHouseSvc.CreateHouse(houseReq);
            return Ok(res);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, GUEST")]
        [HttpPatch("update-house")]
        public IActionResult UpdateHouse([FromBody] PostHouseReq houseReq)
        {
            var res = postHouseSvc.UpdateHouse(houseReq);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpDelete("delete-house/{id}")]
        public IActionResult DeleteHouse(int id)
        {
            var res = postHouseSvc.DeleteHouse(id);
            return Ok(res);
        }


        [HttpPost("search-price-house")]
        public IActionResult SearchHousePrice([FromBody] SearchPriceHouse searchPriceHouse)
        {
            var res = new SingleRsp();
            res.Data = postHouseSvc.SearchHousePrice(searchPriceHouse);
            return Ok(res.Data);
        }

        [HttpPost("search-house")]
        public IActionResult SearchHouseKeyword([FromBody] SearchHouseKeyword k)
        {
            var res = new SingleRsp();
            res.Data = postHouseSvc.SearchHouseKeyword(k);
            return Ok(res.Data);
        }


        [HttpGet("houses")]
        public IActionResult ListHouses()
        {
            return Ok(postHouseSvc.ListHouse());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, GUEST")]
        [HttpPost("MyListHouses")]
        public IActionResult MyListHouses([FromBody] int id)
        {
            return Ok(postHouseSvc.MyListHouse(id));
        }


        [HttpGet("Detail-Post-House/{id}")]
        public IActionResult DetailPostHouse(int id)
        {
            return Ok(postHouseSvc.DetailPostHouse(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpPost("report-house")]
        public IActionResult ReportHouse([FromBody] ReportReq report)
        {
            return Ok(postHouseSvc.ReportHousing(report));
        }
    }
}
