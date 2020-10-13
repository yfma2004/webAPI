using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
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

        public string Pwd { get; set; }



        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonPhone { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 是否在职
        /// </summary>
        public bool? IsIncumbency { get; set; }



        /// <summary>
        /// 最后一次登录IP地址
        /// </summary>
        public string LastLoginIP { get; set; }


        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

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
