using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Out
{
    public class Out_LoginUserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户年龄
        /// </summary>
        public int Age { get; set; }

        public string LoginName { get; set; }

        public string Token { get; set; }
    }
}
