using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
     public class BillNoInfoServices : BaseServices<BillNoInfo>, IBillNoInfoServices
    {
        public string GetBillNo(string billType)
        {
            BillNoInfo billNoInfo = Query(x => x.BillType == billType).OrderBy(x => x.BillNO).ToList().FirstOrDefault();
            if (billNoInfo == null)
            {
                billNoInfo = new BillNoInfo() { BillType = billType, BillNO = 0 };
                billNoInfo.BillNO++;
                Add(billNoInfo);
            }
            else
            {
                billNoInfo.BillNO++;
                Update(billNoInfo);
            }
            string no = billNoInfo.BillNO.ToString();
            no = no.PadLeft(3, '0');
            string billNO = string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyMMdd"), billType, no);
            return billNO;
        }
    }
}
