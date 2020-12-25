using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace lxsShop.NewRepository.IUnitOfWork
{
    public interface IUnitOfWork
    {
        // 获取 SqlSugar 实例
        SqlSugarClient GetDbClient();
        // 事务开始
        void BeginTran();
        // 事务提交
        void CommitTran();
        // 事务回滚
        void RollbackTran();
    }
}
