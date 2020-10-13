using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    
    public class FollowUpRecordInfoServices : BaseServices<FollowUpRecordInfo>, IFollowUpRecordInfoServices
    {

        public int Add(FollowUpRecordInfo model)
        {                     
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            return base.Add(model);
        }

    }
}
