using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class CategoryService : GenericService<article_cats>, ICategoryService
    {

        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
