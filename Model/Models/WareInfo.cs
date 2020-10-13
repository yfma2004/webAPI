using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 商品
    /// </summary>
    public class WareInfo
    {

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string WareID{get;set;}

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }

       
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品PN
        /// </summary>
        public string PN { get; set; }

        /// <summary>
        /// 商品SN
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 商品保修期
        /// </summary>
        public DateTime? WarrantyTime { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        public double TotalMoney { get; set; }


    }
}
