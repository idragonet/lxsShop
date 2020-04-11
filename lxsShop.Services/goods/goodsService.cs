using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class goodsService : GenericService<goods>, IgoodsService
    {

        private readonly IgoodsRepository _repository;
        public goodsService(IgoodsRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
