using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Input
{
    public class MobileInfo
    {

        public string ID { get; set; }
        public string MobileNum { get; set; }
        public string LoginPwd { get; set; }
        public string PayPwd { get; set; }
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string AuthorizationStr { get; set; }
        public string ActionID { get; set; }
        public string IP { get; set; }
        public string Status { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Remark { get; set; }


        public decimal Balance { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime NextOperationTime { get; set; }
        public DateTime UpdateTime { get; set; }

        //邀请码
        public string InviteCode { get; set; }

        //邀请次数
        public int InviteCount { get; set; }

        public string Field1 { get; set; }

        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }

        //操作ip
        public string OperationIP { get; set; }

         

    }
}
