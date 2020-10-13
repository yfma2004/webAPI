using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Input;
using Model.Models;
using Model.Out;
using Model.Search;
using Services;
using Token;
using Token.Model;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : BaseController
    {

        public IHttpContextAccessor Accessor;

        public UserInfoController(IHttpContextAccessor accessor)
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

        public CommonRtnEntity Login([FromBody] UserInfo userInfo)
        {

            //throw new Exception("123");

            IUserServices userServices = new UserServices();

            string pwd = userInfo.Pwd;
            pwd= CommonUtitlity.CommonHelper.GetPwdCryptoStr(pwd);

            UserInfo info = userServices.Query(d =>d.LoginName== userInfo.LoginName&&  d.Pwd == pwd).FirstOrDefault();
            Out_LoginUserInfo outUser =null;
            if (info != null)
            {
                info.LastLoginTime = DateTime.Now;
                info.LastLoginIP = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                userServices.Update(info);


                outUser = new Out_LoginUserInfo();

                outUser.ID = info.ID;
                outUser.Name = info.Name;
                outUser.Age = info.Age;
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
        /// 查询角色
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetList")]
        public CommonRtnEntity GetList([FromBody] SearchBase<UserInfo> searchInfo)
        {
            IUserServices services = new UserServices();

            int totalCount = 0;

            List<UserInfo> list = services.QueryPage(x =>
                x.Name.Contains(searchInfo.Data.Name)
                && x.LoginName.Contains(searchInfo.Data.LoginName)
                && x.PersonPhone.Contains(searchInfo.Data.PersonPhone)
                && x.Email.Contains(searchInfo.Data.Email)
                //&& (searchInfo.Data.IsIncumbency == null || searchInfo.Data.IsIncumbency.Value == x.IsIncumbency)
             , ref totalCount, searchInfo.PageIndex, searchInfo.PageSize, " CreateTime desc ");

            list.ForEach(x => x.Pwd = "");


            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = list.Count > 0,
                Data = new
                {
                    TotalCount = totalCount,
                    Data = list
                },
                Message = "查询成功！"
            };
            return rtnInfo;
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="info">角色信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] UserInfo info)
        {
            IUserServices services = new UserServices();

            int result = 0;
             
            //编辑
            if (info.ID > 0)
            {
                UserInfo oldUserInfo= services.QueryByID(info.ID);
                if (oldUserInfo != null)
                {
                    info.Pwd = oldUserInfo.Pwd;
                }
                info.UpdateTime = DateTime.Now;
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
                Message = result > 0 ? "添加成功！" : "添加失败！"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色id集合</param>      
        /// <returns></returns>
        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] object[] ids)
        {
            IUserServices services = new UserServices();
            bool result = services.DeleteByIds(ids);
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result,
                Data = result,
                Message = result ? "删除成功！" : "删除失败"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetInfo")]
        public CommonRtnEntity GetInfo([FromBody]  SearchBase<string> searchInfo)
        {
            IUserServices services = new UserServices();

            UserInfo orderInfo = services.QueryByID(searchInfo.Data);

            if (orderInfo != null)
            {
                orderInfo.Pwd = "";
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = orderInfo != null,
                Data = new
                {
                    Info = orderInfo
                },
                Message = orderInfo != null ? "查询成功！" : "查询失败！"
            };
            return rtnInfo;
        }





        /// <summary>
        /// 当前用户修改密码，需要原始密码
        /// </summary>
        /// <param name="info">修改密码信息</param>                 
        /// <returns></returns>
        [HttpPost("UpdatePwd")]
        public CommonRtnEntity UpdatePwd([FromBody] InUpdatePwdInfo info)
        {
            bool isSuccess = false;
            string message = "";
            IUserServices services = new UserServices();
            UserInfo _info = services.QueryByID(info.ID);
            if (_info != null)
            {
                string oldPwd = CommonUtitlity.CommonHelper.GetPwdCryptoStr(info.OldPwd);
                if (_info.Pwd != oldPwd)
                {
                    isSuccess = false;
                    message = "旧密码不正确！";
                }
                else
                {
                    _info.Pwd = CommonUtitlity.CommonHelper.GetPwdCryptoStr(info.Pwd);
                    isSuccess = services.Update(_info);
                    message = "修改成功！";
                }
            }
            else
            {
                isSuccess = false;
                message = "未找到用户！";
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = isSuccess,
                Data = isSuccess,
                Message = message
            };
            return rtnInfo;
        }



        /// <summary>
        /// 管理修改密码，不需要原始密码
        /// </summary>
        /// <param name="info">修改密码相关信息</param>         
        /// <returns></returns>
        [HttpPost("UpdatePwdByAdmin")]
        public CommonRtnEntity UpdatePwdByAdmin([FromBody] InUpdatePwdInfo info)
        {
            bool isSuccess = false;
            string message = "";
            IUserServices services = new UserServices();
            UserInfo _info = services.QueryByID(info.ID);
            if (_info != null)
            {
                _info.Pwd = CommonUtitlity.CommonHelper.GetPwdCryptoStr(info.Pwd);
                isSuccess = services.Update(_info);
                message = "修改成功！";
            }
            else
            {
                isSuccess = false;
                message = "未找到用户！";
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = isSuccess,
                Data = isSuccess,
                Message = message
            };
            return rtnInfo;
        }



    }
}
