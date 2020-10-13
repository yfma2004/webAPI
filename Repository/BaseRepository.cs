using IRepository;
using Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return entityDB; }
            private set { entityDB = value; }
        }
        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }



        public TEntity QueryByID(object objId)
        {
            return db.Queryable<TEntity>().InSingle(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public TEntity QueryByID(object objId, bool blnUseCache = false)
        {
            return db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public List<TEntity> QueryByIDs(object[] lstIds)
        {
            return db.Queryable<TEntity>().In(lstIds).ToList();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public int Add(TEntity entity)
        {
            var i = db.Insertable(entity).ExecuteReturnBigIdentity();
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return (int)i;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            //这种方式会以主键为条件
            var i = db.Updateable(entity).ExecuteCommand();
            return i > 0;
        }

        public bool Update(TEntity entity, string strWhere)
        {
            return db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0;
        }



        public bool Update(string strSql)
        {
            SugarParameter[] parameters = null;
            return db.Ado.ExecuteCommand(strSql, parameters) > 0;
        }

        public bool Update(string strSql, SugarParameter[] parameters = null)
        {
            return db.Ado.ExecuteCommand(strSql, parameters) > 0;
        }

        public bool Update(
          TEntity entity,
          List<string> lstColumns = null,
          List<string> lstIgnoreColumns = null,
          string strWhere = ""
            )
        {
            IUpdateable<TEntity> up = db.Updateable(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = up.IgnoreColumns(it => lstIgnoreColumns.Contains(it));
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = up.UpdateColumns(it => lstColumns.Contains(it));
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = up.Where(strWhere);
            }
            return up.ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            var i = db.Deleteable(entity).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            var i = db.Deleteable<TEntity>(id).ExecuteCommand();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids)
        {
            var i = db.Deleteable<TEntity>().In(ids).ExecuteCommand();
            return i > 0;
        }


        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="whereStr">指定条件</param>
        /// <returns></returns>
        public bool DeleteByWhere(string whereStr)
        {
            var i = db.Deleteable<TEntity>().Where(whereStr).ExecuteCommand();
            return i > 0;
        }




        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public List<TEntity> Query()
        {
            return entityDB.GetList();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere)
        {
            return db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {            
            return entityDB.GetList(whereExpression);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList();
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList();
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }


        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds)
        {
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList();
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Blog.Core
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
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList();
        }



        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Blog.Core
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
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Blog.Core
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
            return db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize);
        }




        public List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression)
            .ToPageList(intPageIndex, intPageSize,ref totalNumber);
        }




    }


}
