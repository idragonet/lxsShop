﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using lxsShop.Repository.BaseRepository;
using SqlSugar;

namespace lxsShop.Repository
{
    public abstract class GenericRepository<T> : IDependency, IRepository<T> where T : class, new()
    {
        #region Implementation of IRepository<T>

        public SqlSugarClient db = new DbFactory().GetDb();

        /// <summary>
        /// 根据主值查询单条数据
        /// </summary>
        /// <param name="pkValue">主键值</param>
        /// <returns>泛型实体</returns>
        public T FindById(object pkValue)
        {
          var entity = db.Queryable<T>().InSingle(pkValue);
                return entity;
            
        }

        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> FindAll()
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var list = db.Queryable<T>().ToList();
                return list;
            // }
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var entities = db.Queryable<T>().Where(predicate).ToList();
                return entities;
            // }
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <returns></returns>
        public T FindByClause(Expression<Func<T, bool>> predicate)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var entity = db.Queryable<T>().First(predicate);
                return entity;
            // }
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public long Insert(T entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                //返回插入数据的标识字段值
                var i = db.Insertable(entity).ExecuteReturnBigIdentity();
                return i;
            // }
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                //这种方式会以主键为条件
                var i = db.Updateable(entity).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable(entity).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> @where)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable<T>(@where).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable<T>(id).ExecuteCommand();
                return i > 0;
            // }
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids)
        {
            // using (var db = DbFactory.GetSqlSugarClient())
            // {
                var i = db.Deleteable<T>().In(ids).ExecuteCommand();
                return i > 0;
            // }
        }

        #endregion
    }
}
