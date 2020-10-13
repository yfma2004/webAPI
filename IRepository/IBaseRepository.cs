using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        TEntity QueryByID(object objId);
        TEntity QueryByID(object objId, bool blnUseCache = false);
        List<TEntity> QueryByIDs(object[] lstIds);

        int Add(TEntity model);

        bool DeleteById(object id);

        bool Delete(TEntity model);

        bool DeleteByIds(object[] ids);


        bool DeleteByWhere(string whereStr);
       

        bool Update(TEntity model);
        bool Update(TEntity entity, string strWhere);

        bool Update(string strSql);

        bool Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        List<TEntity> Query();
        List<TEntity> Query(string strWhere);
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        List<TEntity> Query(string strWhere, string strOrderByFileds);
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        List<TEntity> Query(string strWhere, int intTop, string strOrderByFileds);
        List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        List<TEntity> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);
        List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);
    }
}
