using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;

namespace lxsShop.Services
{
   public class CategoryService : ICategoryService
    {

        private readonly IRepository<article_cats> _categoryRepository;
        public CategoryService(IRepository<article_cats> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public article_cats FindById(int id)
        {
            return _categoryRepository.FindById(id);
        }

        public IEnumerable<article_cats> FindAll()
        {
          return  _categoryRepository.FindAll();
        }

        public long Insert(article_cats entity)
        {
            return _categoryRepository.Insert(entity);
        }

        public bool Update(article_cats entity)
        {
            return _categoryRepository.Update(entity);
        }

        public bool Delete(article_cats entity)
        {
            return _categoryRepository.Delete(entity);
        }

        public bool DeleteById(object id)
        {
            return DeleteById(id);
        }

        public bool DeleteByIds(object[] ids)
        {
            return DeleteByIds(ids);
        }
    }
}
