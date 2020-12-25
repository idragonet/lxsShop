using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Masuit.Tools.Logging;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace lxsShop.NewServices
{
   public class DbFactory
    {

        public SqlSugarClient Db; //用来处理事务多表查询和复杂的操作

        private static IConfiguration configure = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        private static readonly string _connectionstring = configure["connectionStrings:Conn"];

        // public BaseHelper(string connectionString)
        public DbFactory()
        {
            Db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = _connectionstring,
                    DbType = DbType.Sqlite,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                });

            //用来打印Sql方便你调式
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {

                LogManager.Debug(sql + "\r\n" +
                                 Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));

                Console.WriteLine(sql + "\r\n" +
                                  Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public SqlSugarClient GetDb()
        {
            return Db;
        }

        public bool InsertInto<T>(T obj) where T : class, new()
        {
            return Db.Insertable(obj).ExecuteCommandIdentityIntoEntity();
        }

        public int UpdateInfo<T>(Expression<Func<T, bool>> set, Expression<Func<T, bool>> where) where T : class, new()
        {
            return Db.Updateable<T>().SetColumns(set).Where(where).ExecuteCommand();
        }
    }
}
