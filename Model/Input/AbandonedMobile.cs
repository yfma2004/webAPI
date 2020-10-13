using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Input
{
    public class AbandonedMobile
    {
        public string ID { get; set; }
        public string MobileNum { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }

        public int Count { get; set; }

        public string ProjectName { get; set; }


    }
}
