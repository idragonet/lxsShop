using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace lxsShop.NewRepository.IBaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IUnitOfWork.IUnitOfWork unitofWork;
        private SqlSugarClient dbBase;

        private ISqlSugarClient _db
        {
            get
            {
                return dbBase;
            }
        }

        internal ISqlSugarClient Db
        {
            get { return _db; }
        }


        public BaseRepository(IUnitOfWork.IUnitOfWork unitofWork)
        {
            this.unitofWork = unitofWork;
            dbBase = unitofWork.GetDbClient();
        }

        public async Task<TEntity> QueryById(object Id)
        {
            return await _db.Queryable<TEntity>().In(Id).SingleAsync();
        }


        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> FindAll()
        {
            return await _db.Queryable<TEntity>().SingleAsync();
        }

    }
}
