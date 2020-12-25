using System;
using System.Collections.Generic;
using System.Text;
using Entitys;
using lxsShop.ViewModel;

namespace lxsShop.Repository
{
   public interface IarticlesRepository : IRepository<articles>
   {
       IEnumerable<articlesViewModel> FindAll();
   }
}
