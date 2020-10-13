using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleInfo
    {

        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
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
