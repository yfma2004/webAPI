using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WebUserInfo
    {
		private Int32 _id;
		private String _name;
		private String _loginname;
		private String _pwd;
		private String _memo;
		private String _phone;
		private String _email;
		private String _usertype;
		private String _wechat;
		private String _receiveaddress;
		private Decimal _balance;
		private String _lastloginip;
		private DateTime _lastlogintime;
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
		public String LoginName
		{
			set { _loginname = value; }
			get { return _loginname; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Pwd
		{
			set { _pwd = value; }
			get { return _pwd; }
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
		public String Phone
		{
			set { _phone = value; }
			get { return _phone; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String Email
		{
			set { _email = value; }
			get { return _email; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String UserType
		{
			set { _usertype = value; }
			get { return _usertype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String WeChat
		{
			set { _wechat = value; }
			get { return _wechat; }
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
		public Decimal Balance
		{
			set { _balance = value; }
			get { return _balance; }
		}
		/// <summary>
		/// 
		/// </summary>
		public String LastLoginIP
		{
			set { _lastloginip = value; }
			get { return _lastloginip; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastLoginTime
		{
			set { _lastlogintime = value; }
			get { return _lastlogintime; }
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
