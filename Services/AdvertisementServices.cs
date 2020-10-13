using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IRepository;
using IServices;
using Model.Models;
using Repository;

namespace Services
{

    public class AdvertisementServices : IAdvertisementServices
    {
        IAdvertisementRepository dal = new AdvertisementRepository();

        public int Add(UserInfo model)
        {
            return dal.Add(model);
        }
        public bool Delete(UserInfo model)
        {
            return dal.Delete(model);
        }

        public List<UserInfo> Query(Expression<Func<UserInfo, bool>> whereExpression)
        {
            return dal.Query(whereExpression);

        }

        public bool Update(UserInfo model)
        {
            return dal.Update(model);
        }

        public int Sum(int i, int j)
        {
            return dal.Sum(i, j);
        }
 
    }
}
