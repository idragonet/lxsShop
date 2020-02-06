using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class goods_catsService : GenericService<goods_cats>, Igoods_catsService
    {

        private readonly Igoods_catsRepository _repository;
        public goods_catsService(Igoods_catsRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
