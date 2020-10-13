using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
     
     public class WareInfoServices : BaseServices<WareInfo>, IWareInfoServices
    {
        public bool DeleteByOrderID(List<string> orderIDList)
        {          
            if (orderIDList.Count>0)
            {
                string whereStr = " orderID in(" + string.Join(",", orderIDList) + ")";
                DeleteByWhere(whereStr);
            }
            else {

            }
            return true;
        }


    }
}
