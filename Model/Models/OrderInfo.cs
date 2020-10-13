using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class OrderInfo
    {
		private Int32 _id;
		private Int32 _userid;
		private DateTime _createtime;
		private DateTime _updatetime;
		private String _gooddesc;
		private String _goodtype;
		private String _delivertype;
		private String _deliverno;
		private String _receiveaddress;
		private Decimal _amount;
		private Decimal _weight;
		private Decimal _size;
		private String _warehouse;
		private String _shiptype;
		private String _shipno;
		private Decimal _shipamount;
		private Decimal _discamount;
		private String _status;
		private String _memo;
		private Int16 _isreceived;
		private DateTime _receivedtime;
		private Int16 _isapproved;
		private DateTime _approvedtime;
		private Int16 _ispaid;
		private DateTime _paidtime;
		private Int16 _isshipped;
		private DateTime _shippedtime;
		private Int16 _isclosed;
		private DateTime _closedtime;
		private String _remark;
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
		public DateTime CreateTime
		{
			set { _createtime = value; }
			get { return _createtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set { _updatetime = value; }
			get { return _updatetime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String GoodDesc
		{
			set { _gooddesc = value; }
			get { return _gooddesc; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String GoodType
		{
			set { _goodtype = value; }
			get { return _goodtype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String DeliverType
		{
			set { _delivertype = value; }
			get { return _delivertype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String DeliverNo
		{
			set { _deliverno = value; }
			get { return _deliverno; }
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
		public Decimal Amount
		{
			set { _amount = value; }
			get { return _amount; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Decimal Weight
		{
			set { _weight = value; }
			get { return _weight; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Decimal Size
		{
			set { _size = value; }
			get { return _size; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Warehouse
		{
			set { _warehouse = value; }
			get { return _warehouse; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String ShipType
		{
			set { _shiptype = value; }
			get { return _shiptype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String ShipNo
		{
			set { _shipno = value; }
			get { return _shipno; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Decimal ShipAmount
		{
			set { _shipamount = value; }
			get { return _shipamount; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Decimal DiscAmount
		{
			set { _discamount = value; }
			get { return _discamount; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Status
		{
			set { _status = value; }
			get { return _status; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Memo
		{
			set { _memo = value; }
			get { return _memo; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsReceived
		{
			set { _isreceived = value; }
			get { return _isreceived; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReceivedTime
		{
			set { _receivedtime = value; }
			get { return _receivedtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsApproved
		{
			set { _isapproved = value; }
			get { return _isapproved; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ApprovedTime
		{
			set { _approvedtime = value; }
			get { return _approvedtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsPaid
		{
			set { _ispaid = value; }
			get { return _ispaid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime PaidTime
		{
			set { _paidtime = value; }
			get { return _paidtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsShipped
		{
			set { _isshipped = value; }
			get { return _isshipped; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ShippedTime
		{
			set { _shippedtime = value; }
			get { return _shippedtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int16 IsClosed
		{
			set { _isclosed = value; }
			get { return _isclosed; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ClosedTime
		{
			set { _closedtime = value; }
			get { return _closedtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Remark
		{
			set { _remark = value; }
			get { return _remark; }
		}
	}
}
