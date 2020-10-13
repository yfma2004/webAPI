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
    public class ChartsController : BaseController
    {


        /// <summary>
        /// 获取订单情况
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetOrderSituation")]
        public CommonRtnEntity GetOrderSituation([FromBody] SearchBase<BuyOrderSearch> searchInfo)
        {

            IBuyOrderInfoServices services = new BuyOrderInfoServices();
            
            DateTime start = DateTime.Parse(DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd"));
            DateTime end = DateTime.Now;

            List<BuyOrderInfo> list = null;
            list = services.Query(d =>
             d.CreateTime>=start  
             && d.CreateTime <= end
             );


            List<string> xList = new List<string>();
            Dictionary<string, List<double>> data = new Dictionary<string, List<double>>();
            data.Add("PO", new List<double>());
            data.Add("SO", new List<double>());
             
            IEnumerable<IGrouping<string, BuyOrderInfo>> query =
             list.GroupBy(pet => pet.CreateTime.Value.ToString("MM-dd"), pet => pet);

            

            for (int i = 0; i < (end-start).TotalDays; i++)
            {
                string dayStr = start.AddDays(i).ToString("MM-dd");
                xList.Add(dayStr);

                List<BuyOrderInfo> todayList = new List<BuyOrderInfo>();

                IGrouping<string, BuyOrderInfo> groupToday= query.Where(x => x.Key == dayStr).FirstOrDefault();
                if (groupToday != null)
                {
                    todayList = groupToday.ToList();
                }
                double poTotalMoney= todayList.Where(x=>x.OrderType=="PO" ).Sum(x => x.TotalMoney);
                double soTotalMoney = todayList.Where(x => x.OrderType == "SO").Sum(x => x.TotalMoney);
                data["PO"].Add(poTotalMoney);
                data["SO"].Add(soTotalMoney);
            }


            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = list.Count > 0,
                Data = new
                {
                    XList = xList,
                    Data= data
                },
                Message = "查询成功！"
            };
            return rtnInfo;
        }





        /// <summary>
        /// 获取销售商品分布情况
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetSaleWareSituation")]
        public CommonRtnEntity GetSaleWareSituation([FromBody] SearchBase<BuyOrderSearch> searchInfo)
        {

            IWareInfoServices services = new WareInfoServices();

            DateTime start = DateTime.Parse(DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd"));
            DateTime end = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

            List<WareInfo> list = services.Query("[OrderID] in (select b.ID from BuyOrderInfo as b where b.OrderType='SO' and b.CreateTime>='" + start.ToString("yyyy-MM-dd") + "' and b.CreateTime<='" + end.ToString("yyyy-MM-dd") + "')");
             
            List<string> wareNameList = new List<string>();
           
            IEnumerable<IGrouping<string, WareInfo>> query =
             list.GroupBy(pet => pet.Name, pet => pet);


            List<Object> dataList = new List<object>();
            foreach (var item in query)
            {
                wareNameList.Add(item.Key);
                dataList.Add(
                    new {
                        value=item.ToList().Sum(x => x.TotalMoney),
                        name=item.Key,
                    }
                    );
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = list.Count > 0,
                Data = new
                {
                    WareNameList = wareNameList,
                    DataList = dataList
                },
                Message = "查询成功！"
            };
            return rtnInfo;
        }






    }
}