﻿using System;
using System.Collections.Generic;
using System.Text;
using Entitys;
using lxsShop.Repository.BaseRepository;
using lxsShop.Services.BaseService;

namespace lxsShop.Services
{
   public interface ICategoryService : IDependency, IService<article_cats>
   {

   }
}
