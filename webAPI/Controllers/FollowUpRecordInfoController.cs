using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Out;
using Model.Search;
using Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpRecordInfoController : BaseController
    {

        /// <summary>
        /// 查询客户供应商跟进记录
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetListByCustomersSuppliersID")]
        public CommonRtnEntity GetListByCustomersSuppliersID([FromBody] SearchBase<int> searchInfo)
        {

            IFollowUpRecordInfoServices services = new FollowUpRecordInfoServices();

            int totalCount = 0;

            List<FollowUpRecordInfo> list = services.QueryPage(x =>
                x.CustomersSuppliersID== searchInfo.Data
             , ref totalCount, searchInfo.PageIndex, searchInfo.PageSize, " CreateTime desc ");

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
        /// 添加客户供应商跟进记录
        /// </summary>
        /// <param name="info">客户供应商跟进记录信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] FollowUpRecordInfo info)
        {
            IFollowUpRecordInfoServices services = new FollowUpRecordInfoServices();

            int result = 0;
            //编辑
            if (info.ID > 0)
            {
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
        /// 删除客户供应商跟进记录
        /// </summary>
        /// <param name="ids">客户供应商id集合</param>      
        /// <returns></returns>
        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] object[] ids)
        {
            IFollowUpRecordInfoServices services = new FollowUpRecordInfoServices();
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
        /// 获取客户供应商跟进记录
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetFollowUpRecordInfo")]
        public CommonRtnEntity GetFollowUpRecordInfo([FromBody]  SearchBase<string> searchInfo)
        {
            IFollowUpRecordInfoServices services = new FollowUpRecordInfoServices();

            FollowUpRecordInfo orderInfo = services.QueryByID(searchInfo.Data);

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
    }
}