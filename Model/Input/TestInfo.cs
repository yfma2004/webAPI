using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Input
{
    /// <summary>
    /// 测试对象信息
    /// </summary>
    public class TestInfo
    {
        /// <summary>
        /// 单据的id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
