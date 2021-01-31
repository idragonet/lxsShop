using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.article
{
    public class IndexModel : PageModel
    {
      
        private readonly IbrandsServer _brandsServer;
        private readonly Isys_configsServer _sysconfigsserver;
        private readonly IbannerServer _bannerserver;
        private readonly IgoodServer _goodserver;
        //   private readonly IBbs_QuestionsService _askService;
        public IndexModel(IbrandsServer brandsServer, Isys_configsServer sysconfigsserver, IbannerServer bannerserver, IgoodServer goodserver)
        {
            _brandsServer = brandsServer;
            _sysconfigsserver = sysconfigsserver;
            _bannerserver = bannerserver;
            _goodserver = goodserver;
            //   _askService = askService;
        }

        public List<brands> brandsList { get; set; }
        public List<banner> bannerList { get; set; }
        public List<goodsViewModel> goodsList_cat2 { get; set; }
        public List<goodsViewModel> goodsList_cat3 { get; set; }

      
        /// <summary>
        /// 问题列表
        /// </summary>
        // public Page<Bbs_Questions> Ask { get; set; }

        public async Task OnGetAsync(string category = null, string key = null, string where = "", int limit = 5)
        {
            ViewData["Title"] =  "关于我们 - "+ await _sysconfigsserver.GetKeyAsync("Title");
        }
    }
}
