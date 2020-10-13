using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Input;
using Model.Models;
using Model.Out;
using Model.Search;
using Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersSuppliersInfoController : BaseController
    {
        /// <summary>
        /// 查询客户供应商
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetList")]
        public CommonRtnEntity GetList([FromBody] SearchBase<CustomersSuppliersSearch> searchInfo)
        {
 
            ICustomersSuppliersInfoServices services = new CustomersSuppliersInfoServices();

            int totalCount = 0;

            List<CustomersSuppliersInfo> list = services.QueryPage(x=> 
                x.Name.Contains(searchInfo.Data.Name)
                && x.Type==searchInfo.Data.Type
                && x.PersonName.Contains( searchInfo.Data.PersonName)
                && x.PersonPhone.Contains(searchInfo.Data.PersonPhone)
                && x.QQ.Contains(searchInfo.Data.QQ)
                && x.WeiXin.Contains(searchInfo.Data.WeiXin)
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
        /// 添加客户供应商
        /// </summary>
        /// <param name="info">客户供应商信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] CustomersSuppliersInfo info)
        {
            ICustomersSuppliersInfoServices services = new CustomersSuppliersInfoServices();

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
        /// 删除客户供应商
        /// </summary>
        /// <param name="ids">客户供应商id集合</param>      
        /// <returns></returns>
        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] object[] ids)
        {
            ICustomersSuppliersInfoServices services = new CustomersSuppliersInfoServices();
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
        /// 获取客户供应商信息
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetCustomersSuppliersInfo")]
        public CommonRtnEntity GetCustomersSuppliersInfo([FromBody]  SearchBase<string> searchInfo)
        {
            ICustomersSuppliersInfoServices services = new CustomersSuppliersInfoServices();

            CustomersSuppliersInfo orderInfo = services.QueryByID(searchInfo.Data);
           
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