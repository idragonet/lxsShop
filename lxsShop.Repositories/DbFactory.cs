﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace lxsShop.Repositories
{
   public class DbFactory
    {

        public SqlSugarClient db;

        private static IConfiguration configure = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        private static readonly string _connectionstring = configure["connectionStrings:Conn"];

        // public BaseHelper(string connectionString)
        public DbFactory()
        {
            db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = _connectionstring,
                    DbType = DbType.Sqlite,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                });

            //用来打印Sql方便你调式
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                                  db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public SqlSugarClient GetDb()
        {
            return db;
        }

        public bool InsertInto<T>(T obj) where T : class, new()
        {
            return db.Insertable(obj).ExecuteCommandIdentityIntoEntity();
        }

        public int UpdateInfo<T>(Expression<Func<T, bool>> set, Expression<Func<T, bool>> where) where T : class, new()
        {
            return db.Updateable<T>().SetColumns(set).Where(where).ExecuteCommand();
        }
    }
}
