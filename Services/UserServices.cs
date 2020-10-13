using CommonUtitlity;
using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
     
    public class UserServices : BaseServices<UserInfo>, IUserServices
    {

        public int Add(UserInfo model)
        {            
            model.Pwd= CommonHelper.GetPwdCryptoStr(ConfigurationUtil.GetSection("DefaultPwd"));
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            return base.Add(model);
        }

        public UserInfo GetUserInfo(string loginName)
        {
            return Query(d => d.LoginName == loginName).FirstOrDefault();
        }




    }
}
