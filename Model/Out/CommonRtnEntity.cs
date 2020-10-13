using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Out
{
   public class CommonRtnEntity
    {
        private object _Data;
        public object Data
        {
            get
            {
                if (this == null)
                {
                    return new object();
                }
                else
                {
                    return _Data;
                }
            }
            set { this._Data = value; }
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
