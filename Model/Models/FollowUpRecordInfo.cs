using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 跟进记录
    /// </summary>
    public class FollowUpRecordInfo
    {
        public int ID { get; set; }


        /// <summary>
        /// 客户供应商ID
        /// </summary>
        public int CustomersSuppliersID { get; set; }

        /// <summary>
        /// 跟进内容
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// 跟进人
        /// </summary>
        public string FollowUpPerson { get; set; }

        /// <summary>
        /// 跟进时间
        /// </summary>
        public DateTime? FollowUpTime { get; set; }



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
