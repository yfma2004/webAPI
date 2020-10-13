using IServices;
using Model.Models;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
    public class BuyOrderInfoServices : BaseServices<BuyOrderInfo>, IBuyOrderInfoServices
    {
        public int Add(BuyOrderInfo model)
        {
            BillNoInfoServices billNOService = new BillNoInfoServices();           
            model.OrderNO = billNOService.GetBillNo(model.OrderType);
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            return base.Add(model);            
        }



        public List<BuyOrderInfo> QueryPage(SearchBase<BuyOrderSearch> searchInfo,out int totalCount)
        {
            totalCount = 0;
            List<BuyOrderInfo> list = null;
            list = QueryPage(d =>
             d.OrderType == searchInfo.Data.OrderType
             && d.OrderNO.Contains(searchInfo.Data.OrderNO)
             && d.CompanyName.Contains(searchInfo.Data.CompanyName)
             && d.PersonName.Contains(searchInfo.Data.PersonName)
             && d.CourierNumber.Contains(searchInfo.Data.CourierNumber)
             && (searchInfo.Data.IsInvoice ==null||  searchInfo.Data.IsInvoice.Value == d.IsInvoice)
             && (searchInfo.Data.IsPay == null || searchInfo.Data.IsPay.Value == d.IsPay)
            
            , ref totalCount, searchInfo.PageIndex, searchInfo.PageSize, " CreateTime desc ");


            return list;
        }



        public bool SetIsPay(object[] ids,bool isPay)
        {
            string updateSql = string.Format("update BuyOrderInfo set isPay={0} where id in ({1})", isPay ? 1 : 0, string.Join(',', ids));
            return Update(updateSql);
        }



        public bool SetIsInvoice(object[] ids, bool isPay)
        {
            string updateSql = string.Format("update BuyOrderInfo set IsInvoice={0} where id in ({1})", isPay ? 1 : 0, string.Join(',', ids));
            return Update(updateSql);
        }
    }
}
