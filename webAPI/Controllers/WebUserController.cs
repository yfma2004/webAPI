using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Out;
using Services;
using Token;
using Token.Model;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebUserController : BaseController
    {
        public IHttpContextAccessor Accessor;
        public WebUserController(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param> 
        /// <param name="pwd">密码</param>      
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]

        public CommonRtnEntity Login([FromBody] WebUserInfo webUserInfo)
        {

            //throw new Exception("123");

            IWebUserServices webUserServices = new WebUserServices();

            string pwd = webUserInfo.Pwd;
            pwd = CommonUtitlity.CommonHelper.GetPwdCryptoStr(pwd);

            WebUserInfo info = webUserServices.Query(d => d.LoginName == webUserInfo.LoginName && d.Pwd == pwd).FirstOrDefault();
            Out_LoginUserInfo outUser = null;
            if (info != null)
            {
                info.LastLoginTime = DateTime.Now;
                info.LastLoginIP = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                webUserServices.Update(info);


                outUser = new Out_LoginUserInfo();

                outUser.ID = info.ID;
                outUser.Name = info.Name;
                //outUser.Age = info.Age;
                outUser.LoginName = info.LoginName;
                outUser.ID = info.ID;

                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                //var claims = new Claim[] { new Claim(ClaimTypes.Name, User.LoginName), new Claim(ClaimTypes.Role, "Sys"), new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                ////用户标识
                //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                //identity.AddClaims(claims);

                TokenModel tokenModel = new TokenModel();
                tokenModel.Uid = info.ID;
                tokenModel.Uname = info.LoginName;
                tokenModel.Sub = "admin";


                outUser.Token = JwtToken.IssueJWT(tokenModel);

            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = outUser != null,
                Data = outUser,
                Message = outUser != null ? "登录成功！" : "账号或者密码错误！"
            };
            return rtnInfo;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public CommonRtnEntity Register([FromBody] WebUserInfo info)
        {
            IWebUserServices services = new WebUserServices();

            int result = 0;

            //编辑
            if (info.ID > 0)
            {
                WebUserInfo oldUserInfo = services.QueryByID(info.ID);
                if (oldUserInfo != null)
                {
                    info.Pwd = oldUserInfo.Pwd;
                }
                info.LastLoginTime = DateTime.Now;
                services.Update(info);
                result = info.ID;
            }
            else
            {
                result = services.Add(info);
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "注册成功！" : "注册失败！"
            };
            return rtnInfo;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>

        [HttpPost("FindPwd")]
        [AllowAnonymous]
        public CommonRtnEntity FindPwd([FromBody] WebUserInfo info) {
            int result = 0;

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "添加成功！" : "添加失败！"
            };
            return rtnInfo;
        }
    }
}
