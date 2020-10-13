using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Search
{
    /// <summary>
    /// 采购订单查询参数类
    /// </summary>
    public class BuyOrderSearch
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }
        
        /// <summary>
        /// 客户供应商名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string PersonName { get; set; }

       
        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNumber { get; set; }

         
        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }

        /// <summary>
        /// 是否开票
        /// </summary>
        public bool? IsInvoice { get; set; }

        /// <summary>
        /// 订单类型 PO采购  SO销售
        /// </summary>
        public string OrderType { get; set; }

    }
}
