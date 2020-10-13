using IServices;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CustomersSuppliersInfoServices : BaseServices<CustomersSuppliersInfo>, ICustomersSuppliersInfoServices
    {

        public int Add(CustomersSuppliersInfo model)
        {
            CustomersSuppliersInfoServices billNOService = new CustomersSuppliersInfoServices();
        
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            return base.Add(model);
        }
    }
}
