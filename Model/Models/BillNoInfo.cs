using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class BillNoInfo
    {
        public int ID { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string BillType { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int BillNO { get; set; }
    }
}
