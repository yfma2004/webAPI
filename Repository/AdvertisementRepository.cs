using IRepository;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Model.Models;
using Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<UserInfo> entityDB;

        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public AdvertisementRepository()
        { 
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<UserInfo>(db);
        }



        public int Add(UserInfo model)
        {
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            var i = db.Insertable(model).ExecuteReturnBigIdentity();
            return i.ObjToInt();
        }

        public bool Delete(UserInfo model)
        {
            var i = db.Deleteable(model).ExecuteCommand();
            return i > 0;
        }

        public List<UserInfo> Query(Expression<Func<UserInfo, bool>> whereExpression)
        {
            return entityDB.GetList(whereExpression);
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public bool Update(UserInfo model)
        {
            //这种方式会以主键为条件
            var i = db.Updateable(model).ExecuteCommand();
            return i > 0;
        }
    }
}
