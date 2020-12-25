using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace lxsShop.NewRepository.IUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient sqlSugarClient;

        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            this.sqlSugarClient = sqlSugarClient;
        }

        public SqlSugarClient GetDbClient()
        {
            return sqlSugarClient as SqlSugarClient;
        }

        public void BeginTran()
        {
            GetDbClient().BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDbClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().RollbackTran();
                throw ex;
            }
        }


        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }

    }
}
