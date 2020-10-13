using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
  
    public class RoleInfoServices : BaseServices<RoleInfo>, IRoleInfoServices
    {
        public int Add(RoleInfo model)
        {
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            return base.Add(model);
        }
    }
}
