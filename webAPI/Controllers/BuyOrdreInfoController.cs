using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize("Admin")]
    
    public class BuyOrdreInfoController : BaseController
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetList")]
        public CommonRtnEntity GetList([FromBody] SearchBase<BuyOrderSearch> searchInfo)
        {

            BuyOrderInfoServices services = new BuyOrderInfoServices();

            int totalCount = 0;

            //string name= CurrentUserInfo.Name ;

            List<BuyOrderInfo> list = services.QueryPage(searchInfo ,out totalCount);

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = list.Count>0,
                Data = new
                {
                    TotalCount = totalCount,
                    Data = list
                },
                Message =  "查询成功！"
            };
            return rtnInfo;
        }


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="info">订单信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] InOrderInfo info)
        {
            IBuyOrderInfoServices services = new BuyOrderInfoServices();

            int result = 0;
            //编辑
            if (info.Info.ID > 0)
            {
                info.Info.UpdateTime = DateTime.Now;
                services.Update(info.Info);
                result = info.Info.ID;

                List<WareInfo> wareList = (new WareInfoServices()).Query(ware => ware.OrderID == info.Info.ID);
                if (wareList.Count > 0)
                {
                    (new WareInfoServices()).DeleteByIds(wareList.Select(x => x.ID + "").ToArray());
                }
            }
            else
            {
                result = services.Add(info.Info);
            }
           

            if (result > 0)
            {
                foreach (var item in info.WareList)
                {
                    item.OrderID = result;
                    (new WareInfoServices()).Add(item);
                }                
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result>0 ? "添加成功！":"添加失败！"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="ids">订单id集合</param>      
        /// <returns></returns>
        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] object [] ids)
        {
            IBuyOrderInfoServices services = new BuyOrderInfoServices();            
            bool result= services.DeleteByIds(ids);
            if (result)
            {
                //删除订单也需要删除商品
                WareInfoServices wareServices = new WareInfoServices();

                List<string> orderIDList = new List<string>();
                for (int i = 0; i < ids.Length; i++)
                {
                    orderIDList.Add(ids[i] + "");
                }

                wareServices.DeleteByOrderID(orderIDList);
            }
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result,
                Data = result,
                Message =result ? "删除成功！":"删除失败"
            };
            return rtnInfo;
        }


        /// <summary>
        /// 标识订单已付款
        /// </summary>
        /// <param name="ids">订单id集合</param>      
        /// <returns></returns>
        [HttpPost("SetIsPay")]
        public CommonRtnEntity SetIsPay([FromBody] object[] ids)
        {
            BuyOrderInfoServices services = new BuyOrderInfoServices();            
            bool result = services.SetIsPay(ids,true);
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result,
                Data = result,
                Message = result ? "标记付款成功！" : "标记付款失败"
            };
            return rtnInfo;
        }

        /// <summary>
        /// 标识订单已付款
        /// </summary>
        /// <param name="ids">订单id集合</param>      
        /// <returns></returns>
        [HttpPost("SetIsInvoice")]
        public CommonRtnEntity SetIsInvoice([FromBody] object[] ids)
        {
            BuyOrderInfoServices services = new BuyOrderInfoServices();
            bool result = services.SetIsInvoice(ids, true);
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result,
                Data = result,
                Message = result ? "标记开票成功！" : "标记开票失败"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetBuyOrderInfo")]
        public CommonRtnEntity GetBuyOrderInfo([FromBody]  SearchBase<string> searchInfo)
        {
            IBuyOrderInfoServices services = new BuyOrderInfoServices();

            BuyOrderInfo orderInfo= services.QueryByID(searchInfo.Data);

            List<WareInfo> wareList = new List<WareInfo>();
            if (orderInfo!=null)
            {
                wareList = (new WareInfoServices()).Query(ware => ware.OrderID == orderInfo.ID);               
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = orderInfo!=null,
                Data = new {
                    Info= orderInfo,
                    WareList= wareList
                },
                Message = orderInfo != null ? "查询成功！" : "查询失败！"
            };
            return rtnInfo;
        }


    }
}
