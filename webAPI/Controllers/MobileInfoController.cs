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
using SqlSugar;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileInfoController : BaseController
    {
        static string GetInfoByStatusAndUpdateDoingLockStr = "";

        static string GetInfoByLessThanInviteCountAndUpdateDoingLockStr = "";

        /// <summary>
        /// 根据手机号获取信息
        /// </summary>
        /// <param name="clientName">客户端</param>      
        /// <param name="projectName">项目名</param>      
        /// <param name="mobileNum">账号</param>      
        /// <returns></returns>
        [HttpGet("GetInfoByMobile")]
        public  CommonRtnEntity Get(string clientName, string projectName,string mobileNum)
        {
            IMobileInfoServices advertisementServices = new MobileInfoServices();
            MobileInfo info =  advertisementServices.Query(d => d.MobileNum == mobileNum&&d.ProjectName== projectName&&d.ClientName== clientName).FirstOrDefault();

            //((MobileInfoServices)advertisementServices).MyTest();

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = info!=null,
                Data = info,
                Message = "查询成功！"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 检查是否自己手机号
        /// </summary>
        /// <param name="clientName">客户端</param>    
        /// <param name="projectName">项目名</param>  
        /// <param name="mobileNum">手机号</param>      
        /// <returns></returns>
        [HttpGet("CheckIsMyMobile")]
        public   CommonRtnEntity CheckIsMyMobile(string clientName, string projectName, string mobileNum)
        {
            bool isMy = false;
            //先查是否在自己号码库中
            IMobileInfoServices advertisementServices = new MobileInfoServices();
            MobileInfo info = advertisementServices.Query(d => d.MobileNum == mobileNum&&d.ProjectName==projectName&&d.ClientName==clientName).FirstOrDefault();
            string msg = "";
            if (info != null)
            {
                msg = "已使用！";
                isMy = true;              
            }
            else {
                //如果不在自己号码库中，再查是否作废
                IAbandonedMobileServices abandonedMobileServices = new AbandonedMobileServices();
                AbandonedMobile abandonedMobile = abandonedMobileServices.Query(d => d.MobileNum == mobileNum && d.ProjectName == projectName).FirstOrDefault();
                if (abandonedMobile != null)
                {
                    abandonedMobile.Count++;
                      abandonedMobileServices.Update(abandonedMobile);
                    isMy = true;
                    msg = "已作废！";
                }
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = isMy,
                Data = msg,
                Message = "检查完毕！"
            };
            return rtnInfo;
        }


        /// <summary>
        /// 根据某种状态获取手机号并且修改为占用
        /// </summary>
        /// <param name="clientName">客户端</param>   
        /// <param name="projectName">项目名</param>   
        /// <param name="status">当前状态</param>   
        /// <param name="isUseNextOperationTime">是否使用下一次操作时间</param>   
        /// <returns></returns>
        [HttpGet("GetInfoByStatusAndUpdateDoing")]
        public   CommonRtnEntity GetInfoByStatusAndUpdateDoing(string clientName, string projectName,string status, bool isUseNextOperationTime=false)
        {

            lock (GetInfoByStatusAndUpdateDoingLockStr)
            {
                IMobileInfoServices advertisementServices = new MobileInfoServices();

                List<MobileInfo> list = new List<MobileInfo>();
                if (isUseNextOperationTime)
                {
                    list = advertisementServices.Query(d => d.Status == status && d.ProjectName == projectName && d.ClientName == clientName && d.NextOperationTime < DateTime.Now);
                }
                else
                {
                    list = advertisementServices.Query(d => d.Status == status && d.ProjectName == projectName && d.ClientName == clientName);
                }
                MobileInfo info = null;
                if (list != null && list.Count > 0)
                {
                    info = list[(new Random()).Next(0, list.Count - 1)];
                    info.Status = "占用";
                    if (info.CreateTime < DateTime.Now.AddYears(-5))
                    {
                        info.CreateTime = DateTime.Now;
                    }
                    if (info.NextOperationTime < DateTime.Now.AddYears(-5))
                    {
                        info.NextOperationTime = DateTime.Now.AddHours(3);
                    }
                    info.UpdateTime = DateTime.Now;
                    advertisementServices.Update(info);
                }

                string errorMsg = "暂未获取到！";

                if (isUseNextOperationTime)
                {
                    MobileInfo nextOperationInfo= advertisementServices.Query(d => d.Status == status && d.ProjectName == projectName && d.ClientName == clientName).OrderBy(x => x.NextOperationTime).FirstOrDefault();
                    if (nextOperationInfo != null)
                    {
                        errorMsg = "成熟时间：" + nextOperationInfo.NextOperationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                CommonRtnEntity rtnInfo = new CommonRtnEntity()
                {
                    Success = info != null,
                    Data = info,
                    Message = info != null ? "获取成功！" : errorMsg
                };
                return rtnInfo;
            }
        }


        /// <summary>
        /// 根据小于分享次数获取手机号并且修改为占用
        /// </summary>
        /// <param name="clientName">客户端</param>   
        /// <param name="projectName">项目名</param>   
        /// <param name="inviteCount">分享次数最大值（不包含）</param>          
        /// <returns></returns>
        [HttpGet("GetInfoByLessThanInviteCountAndUpdateDoing")]
        public CommonRtnEntity GetInfoByLessThanInviteCountAndUpdateDoing(string clientName, string projectName,int inviteCount, string status="0")
        {

            lock (GetInfoByLessThanInviteCountAndUpdateDoingLockStr)
            {
                IMobileInfoServices advertisementServices = new MobileInfoServices();

                List<MobileInfo> list = new List<MobileInfo>();

                list = advertisementServices.Query(d => d.ProjectName == projectName && d.ClientName == clientName && d.Status == status && !SqlFunc.IsNullOrEmpty(d.InviteCode)).Take(5).ToList();
                list = list.Where(x => x.InviteCount < inviteCount).ToList();
               
                MobileInfo info = null;
                if (list != null && list.Count > 0)
                {
                    info = list[(new Random()).Next(0, list.Count - 1)];
                    info.Status = "占用";
                    if (info.CreateTime < DateTime.Now.AddYears(-5))
                    {
                        info.CreateTime = DateTime.Now;
                    }                    
                    info.UpdateTime = DateTime.Now;
                    advertisementServices.Update(info);
                }

                string errorMsg = "暂未获取到！";

                CommonRtnEntity rtnInfo = new CommonRtnEntity()
                {
                    Success = info != null,
                    Data = info,
                    Message = info != null ? "获取成功！" : errorMsg
                };
                return rtnInfo;
            }
        }
         

        /// <summary>
        /// 根据项目名和号码修改多个字段
        /// </summary>  
        /// <param name="valueDir">键值对，必须包含ClientName，ProjectName和MobileNum</param>             
        /// <returns></returns>
        [HttpPost("UpdateInfoByMobile")]
        public CommonRtnEntity UpdateInfoByMobile([FromBody] Dictionary<string,string> valueDir)
        { 
            IMobileInfoServices advertisementServices = new MobileInfoServices();
            MobileInfo info = advertisementServices.Query(d => d.MobileNum == valueDir["MobileNum"]&& d.ProjectName == valueDir["ProjectName"]&&d.ClientName == valueDir["ClientName"]).FirstOrDefault();

            bool success = false;

            if (info != null)
            {
                foreach (var item in valueDir)
                {

                    Type type = info.GetType(); //获取类型                   
                    System.Reflection.PropertyInfo propertyInfo = type.GetProperty(item.Key); //获取指定名称的属性
                    if (propertyInfo != null)
                    {
                        object value = Convert.ChangeType(item.Value, propertyInfo.PropertyType);
                        propertyInfo.SetValue(info, value, null); //给对应属性赋值                       
                    }
                }          
                if (info.CreateTime < DateTime.Now.AddYears(-5))
                {
                    info.CreateTime = DateTime.Now;
                }
                info.UpdateTime = DateTime.Now;
                success = advertisementServices.Update(info);
            }            

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = success,
                Data = success ? info:null,
                Message = success ? "修改成功！" : "修改失败！",
            };
            return rtnInfo;
        }




        /// <summary>
        /// 添加手机号信息
        /// </summary>
        /// <param name="info">对象信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] MobileInfo info)
        {
            CommonRtnEntity rtnInfo = new CommonRtnEntity();
            try
            {
                IMobileInfoServices advertisementServices = new MobileInfoServices();
                info.CreateTime = DateTime.Now;
                info.UpdateTime = DateTime.Now;
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

        
    }
}
