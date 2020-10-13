using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Out;
using Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// 获取邮箱内容
        /// </summary>
        /// <param name="userName">邮箱账号</param>
        /// <param name="pwd">邮箱密码</param>
        /// <param name="keyWords">邮箱内容关键字</param>
        /// <param name="tryCount">获取超时次数，1秒1次</param>
        /// <returns></returns>
        [HttpGet("GetEmailContent")]
        public CommonRtnEntity GetEmailContent(string userName, string pwd, string keyWords, int tryCount = 1)
        {
            CommonRtnEntity rtnInfo = new CommonRtnEntity();
            string rtnmsg = "";
            bool result = true;
            if (string.IsNullOrEmpty(userName))
            {
                result = false;
                rtnmsg = "邮箱账号不能为空！";
            }
            else if (string.IsNullOrEmpty(pwd))
            {
                result = false;
                rtnmsg = "邮箱密码不能为空！";
            }

            if (result)
            {
                IEmailServices advertisementServices = new EmailServices();
                string content = "";
                result = advertisementServices.GetEmailContent(userName, pwd, keyWords, out content, out rtnmsg, tryCount);
                rtnInfo.Success = result;
                rtnInfo.Data = content;
                rtnInfo.Message = result ? "获取成功！" : rtnmsg;
            }
            else {
                rtnInfo.Success = result;
                rtnInfo.Data = "";
                rtnInfo.Message = rtnmsg;
            } 
            return rtnInfo;
        }

        // POST: api/Email
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
