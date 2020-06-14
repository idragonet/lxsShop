using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class brandsService : GenericService<brands>, IbrandsService
    {

        private readonly IbrandsRepository _repository;

        public brandsService(IbrandsRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
