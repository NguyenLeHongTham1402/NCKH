using Blog.BLL;
using Blog.Common.Req;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserSvc userSvc;
        private readonly JWTConfig _jWTConfig;
        public UserController(IOptions<JWTConfig> jWTConfig)
        {
            userSvc = new UserSvc();
            _jWTConfig = jWTConfig.Value;
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody] UserReq userReq)
        {
            var res = userSvc.CreateUser(userReq);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpGet("GetAllUsers")]       
        public IActionResult ListUsers()
        {
            return Ok(userSvc.ListUsers());
        }

        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody] UserReq userReq)
        {
            var res = userSvc.LoginUser(userReq);
            if (res.Success == true)
            {
                var u = (UserReq)res.Data;
                u.Token = GenerateToken(u);
                res.Data = u;
            }           
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var res = userSvc.DeleteUser(id);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpPost("FindUserById")]
        public IActionResult FindUserById([FromBody] UserReq userReq)
        {
            return Ok(userSvc.FindUserByID(userReq.UserId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpPost("report-user")]
        public IActionResult ReportUser([FromBody] ReportReq report)
        {
            return Ok(userSvc.ReportUser(report));
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, GUEST")]
        [HttpPatch("update-user")]
        public IActionResult UpdateUser([FromBody] UserReq userReq)
        {
            var res = userSvc.UpdateUser(userReq);
            return Ok(res);
        }

        private string GenerateToken(UserReq user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.key);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.ContactName),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.audience,
                Issuer=_jWTConfig.issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
