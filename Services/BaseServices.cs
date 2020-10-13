using IRepository;
using IServices;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();

        public TEntity QueryByID(object objId)
        {
            return baseDal.QueryByID(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public TEntity QueryByID(object objId, bool blnUseCache = false)
        {
            return baseDal.QueryByID(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public List<TEntity> QueryByIDs(object[] lstIds)
        {
            return baseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public int Add(TEntity entity)
        {
            return baseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            return baseDal.Update(entity);
        }
        public bool Update(TEntity entity, string strWhere)
        {
            return baseDal.Update(entity, strWhere);
        }

        public bool Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            return baseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            return baseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids)
        {
            return baseDal.DeleteByIds(ids);
        }



        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="whereStr">指定条件</param>
        /// <returns></returns>
        public bool DeleteByWhere(string whereStr)
        {
            return baseDal.DeleteByWhere(whereStr);
        }


        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public List<TEntity> Query()
        {
            return baseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere)
        {
            return baseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return baseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return baseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return baseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return baseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return baseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return baseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return baseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        public List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return baseDal.QueryPage(whereExpression,ref totalNumber,
            intPageIndex,intPageSize, strOrderByFileds);
        }

        public bool Update(string strSql)
        {
            return baseDal.Update(strSql);
        }
    }
}