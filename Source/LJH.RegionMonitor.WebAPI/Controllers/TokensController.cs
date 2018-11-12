using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LJH.GeneralLibrary.Core;

namespace LJH.RegionMonitor.WebAPI
{
    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        #region 构造函数
        public TokensController(ILoggerFactory loggerFactory, IConfiguration config)
        {
            _ILoggerFactory = loggerFactory;
            Configuration = config;
        }
        #endregion

        #region 私有变量
        private ILoggerFactory _ILoggerFactory;
        private IConfiguration Configuration;
        #endregion

        #region 公共方法
        [HttpPost]
        public virtual IActionResult CreateToken([FromBody]TokenRequst info)
        {
            if (ModelState.IsValid)
            {
                var item = new { Name = "admin", Password = "123", RoleID = "SYS" };
                if (item == null || (item.Name != info.UserID && item.Password != info.Password)) return Ok(new CommandResult(ResultCode.Fail, "用户名或密码不正确"));
                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,item.Name ),
                    new Claim(ClaimTypes.Role,item.RoleID),
               };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                DateTime dt = DateTime.Now.AddMinutes(30);
                var token = new JwtSecurityToken(Configuration["ValidIssuer"], Configuration["ValidAudience"], claims, DateTime.Now, dt, creds);

                var obj = new TokenInfo()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireTime = dt,
                    Sign = string.Empty
                };
                return Ok(new CommandResult<TokenInfo>(ResultCode.Successful, string.Empty, obj));
            }
            return Ok(new CommandResult(ResultCode.Fail, "参数错误"));
        }
        #endregion
    }

    public class TokenRequst
    {
        public string UserID { get; set; }

        public string Password { get; set; }

        public int Timestamp { get; set; }

        public string Sign { get; set; }
    }

    public class TokenInfo
    {
        #region 构造函数
        public TokenInfo()
        {
        }
        #endregion

        #region 公共属性
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }

        public string Sign { get; set; }
        #endregion
    }
}
