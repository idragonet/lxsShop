using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class article_catsService : GenericService<article_cats>, Iarticle_catsService
    {

        private readonly Iarticle_catsRepository _repository;
        public article_catsService(Iarticle_catsRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
