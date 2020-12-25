using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lxsShop.NewRepository.IBaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(object Id);
        Task<TEntity> FindAll();
    }
}
