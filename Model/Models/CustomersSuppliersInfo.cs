using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 客户供应商
    /// </summary>
    public class CustomersSuppliersInfo
    {
        public int ID { get; set; }

        /// <summary>
        /// 客户供应商名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型  C客户   S供应商
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonPhone { get; set; }


        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeiXin { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Addreass { get; set; }



        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get; set; }


        /// <summary>
        /// 信用度
        /// </summary>
        public string Credit { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }


        /// <summary>
        /// 创建人编码
        /// </summary>
        public string CreaterCode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreaterName { get; set; }


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
