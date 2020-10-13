using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Search
{
    /// <summary>
    /// 客户供应商查询参数类
    /// </summary>
    public class CustomersSuppliersSearch
    {

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

    }
}
