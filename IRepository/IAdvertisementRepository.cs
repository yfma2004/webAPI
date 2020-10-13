using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IRepository
{
    public interface IAdvertisementRepository
    {
        int Sum(int i, int j);

        int Add(UserInfo model);
        bool Delete(UserInfo model);
        bool Update(UserInfo model);
        List<UserInfo> Query(Expression<Func<UserInfo, bool>> whereExpression);

    }
}
