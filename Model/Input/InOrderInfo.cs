using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Input
{
    public class InOrderInfo
    {
        public BuyOrderInfo Info { get; set; }
        public  List<WareInfo> WareList { get; set; }
    }
}
