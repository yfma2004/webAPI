using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Input
{
    public class InUpdatePwdInfo
    {
        public int ID { get; set; }

        public string OldPwd { get; set; }

        public string Pwd { get; set; }
        public string Pwd1 { get; set; }
    }
}
