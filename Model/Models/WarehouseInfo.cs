using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class WarehouseInfo
    {
		private Int32 _id;
		private String _name;
		private String _receiveaddress;
		private DateTime _createtime;
		private Int16 _islocked;
		/// <summary>
		/// 
		/// </summary>
		public Int32 ID
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Name
		{
			set { _name = value; }
			get { return _name; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String ReceiveAddress
		{
			set { _receiveaddress = value; }
			get { return _receiveaddress; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set { _createtime = value; }
			get { return _createtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsLocked
		{
			set { _islocked = value; }
			get { return _islocked; }
		}
	}
}
