using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Entitys;
using MySql.Data.MySqlClient;
using SqlSugar;

namespace lxsShop.Repository
{
   public class CategoryRepository : ICategoryRepository
    {

       private SqlSugarClient db = new DbFactory().GetDb();

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <returns></returns>
        public article_cats FindById(int id)
        {

            /*using (var db = DbFactory.GetSqlSugarClient())
            {*/
                var entity = db.Queryable<article_cats>().Single(x => x.catId == id);
                return entity;
            /*}*/
        }

        /// <summary>
        /// 查询所有数据(无分页,大数量时请慎用)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<article_cats> FindAll()
        {
            #region SqlSugar读取方式
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var list = db.Queryable<article_cats>().ToList();
                return list;
            // }
            #endregion
        }


        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public int Insert(article_cats entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Insertable(entity).ExecuteReturnBigIdentity();
                //返回的i是long类型,这里你可以根据你的业务需要进行处理
                return (int)i;
            // }
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Update(article_cats entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                //这种方式会以主键为条件
                var i = db.Updateable(entity).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public bool Delete(article_cats entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable(entity).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable<article_cats>(id).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable<article_cats>().In(ids).ExecuteCommand();
                return i > 0;
            // }
        }

    }
}
