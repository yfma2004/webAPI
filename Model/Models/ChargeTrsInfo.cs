using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class ChargeTrsInfo
    {
		private Int32 _id;
		private Int32 _userid;
		private Decimal _amount;
		private String _paidtype;
		private DateTime _createtime;
		private String _memo;
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
		public Int32 UserID
		{
			set { _userid = value; }
			get { return _userid; }
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
		public String PaidType
		{
			set { _paidtype = value; }
			get { return _paidtype; }
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
		public String Memo
		{
			set { _memo = value; }
			get { return _memo; }
		}
	}
}
