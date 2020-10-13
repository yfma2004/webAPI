using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 订单信息的基类
    /// </summary>
    public class OrderInfoBase
    {

        public int ID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }


        /// <summary>
        /// 订单类型 SO PO
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 客户供应商编码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 客户供应商名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonPhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Addreass { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNumber{get;set;}

        /// <summary>
        /// 订单发货收货日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 是否付款
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 是否开票
        /// </summary>
        public bool IsInvoice { get; set; }

        /// <summary>
        /// 创建人编码
        /// </summary>
        public string CreaterCode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreaterName { get; set; }

        /// <summary>
        /// 保修时间
        /// </summary>
        public DateTime? WarrantyTime { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

 

    }
}
 