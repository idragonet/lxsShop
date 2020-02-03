using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

namespace lxsShop.Services
{
   public interface ICategoryService
    {
        /// <summary>
        /// 根据ID查询单条数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        article_cats FindById(int id);
        /// <summary>
        /// 查询所有数据(无分页,大数量时请慎用)
        /// </summary>
        /// <returns></returns>
        IEnumerable<article_cats> FindAll();

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        int Insert(article_cats entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        bool Update(article_cats entity);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        bool Delete(article_cats entity);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool DeleteById(object id);

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        bool DeleteByIds(object[] ids);

    }
}
