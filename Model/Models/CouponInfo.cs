using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class CouponInfo
    {
		private Int32 _id;
		private String _code;
		private String _name;
		private Decimal _amount;
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
		public String Code
		{
			set { _code = value; }
			get { return _code; }
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
		public Decimal Amount
		{
			set { _amount = value; }
			get { return _amount; }
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
