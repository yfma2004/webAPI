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
using Model.Search;
using Services;
using Token;
using Token.Model;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        public IHttpContextAccessor Accessor;
        public OrderController(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        [HttpPost("List")]
        public CommonRtnEntity List([FromBody] SearchBase<OrderInfo> searchInfo)
        {
            int result = 0;
            int totalCount = 0;
            IOrderServices services = new OrderServices();

            List<OrderInfo> list = services.QueryPage(x =>
                x.ID.Equals(searchInfo.Data.ID)
                && x.UserID.Equals(searchInfo.Data.UserID)
                && x.DeliverType.Equals(searchInfo.Data.DeliverType)
                && x.DeliverNo.Contains(searchInfo.Data.DeliverNo)
             //&& (searchInfo.Data.IsIncumbency == null || searchInfo.Data.IsIncumbency.Value == x.IsIncumbency)
             , ref totalCount, searchInfo.PageIndex, searchInfo.PageSize, " CreateTime desc ");

            //list.ForEach(x => x.Pwd = "");

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
        /// 添加订单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] OrderInfo info)
        {
            IOrderServices services = new OrderServices();

            int result = 0;

            //编辑
            if (info.ID > 0)
            {
                OrderInfo oldOrderInfo = services.QueryByID(info.ID);
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
                Message = result > 0 ? "成功" : "失败"
            };
            return rtnInfo;
        }

        [HttpPost("Update")]
        public CommonRtnEntity Update([FromBody] OrderInfo info)
        {
            int result = 0;

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "获取成功" : "获取失败"
            };
            return rtnInfo;
        }

        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] OrderInfo info)
        {
            IOrderServices services = new OrderServices();

            int result = 0;

            //编辑
            if (info.ID > 0)
            {
                result=services.DeleteById(info.ID)==true?1:0;
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "成功" : "失败"
            };
            return rtnInfo;
        }
        [HttpPost("Detail")]
        public CommonRtnEntity Detail([FromBody] OrderInfo info)
        {
            int result = 0;

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "获取成功" : "获取失败"
            };
            return rtnInfo;
        }
    }
}
