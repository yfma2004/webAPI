using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class PaymentTrsInfo
    {
		private Int32 _id;
		private Int32 _orderid;
		private Decimal _amount;
		private Decimal _paidamount;
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
		public Int32 OrderID
		{
			set { _orderid = value; }
			get { return _orderid; }
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
		public Decimal PaidAmount
		{
			set { _paidamount = value; }
			get { return _paidamount; }
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
