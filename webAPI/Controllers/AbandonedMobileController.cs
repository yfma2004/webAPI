using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Input;
using Model.Out;
using Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbandonedMobileController : BaseController
    {

        /// <summary>
        /// 根据手机号获取信息
        /// </summary>
        /// <param name="projectName">项目名</param> 
        /// <param name="mobileNum">账号</param>      
        /// <returns></returns>
        [HttpGet("GetInfoByMobile")]
        public CommonRtnEntity Get(string projectName, string mobileNum)
        {
            IAbandonedMobileServices advertisementServices = new AbandonedMobileServices();
            AbandonedMobile info = advertisementServices.Query(d => d.MobileNum == mobileNum&&d.ProjectName==projectName).FirstOrDefault();
            if (info != null)
            {
                info.Count++;
                advertisementServices.Update(info);
            }
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = info != null,
                Data = info,
                Message = "查询成功！"
            };
            return rtnInfo;
        }

        /// <summary>
        /// 添加手机号信息
        /// </summary>
        /// <param name="info">对象信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] AbandonedMobile info)
        {
            CommonRtnEntity rtnInfo = new CommonRtnEntity();
            try
            {
                IAbandonedMobileServices advertisementServices = new AbandonedMobileServices();
                info.Count = 0;
                info.CreateTime = DateTime.Now;
                int result = advertisementServices.Add(info);
                rtnInfo.Success = result > 0;
                rtnInfo.Data = result;
                rtnInfo.Message = "添加成功！";
            }
            catch (Exception ex)
            {
                rtnInfo.Success = false;
                rtnInfo.Data = ex.Message;
                rtnInfo.Message = ex.Message;
            }

            return rtnInfo;
        }

        // PUT: api/AbandonedMobile/5
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
