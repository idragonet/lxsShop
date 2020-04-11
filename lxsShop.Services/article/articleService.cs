using System;
using System.Collections.Generic;
using System.Text;
using Entitys;

using lxsShop.Repository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public class articleService : GenericService<articles>, IarticleService
    {

        private readonly IarticlesRepository _repository;

        public articleService(IarticlesRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
