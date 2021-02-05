using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.catalog
{


    public class IndexModel : PageModel
    {

        private readonly Isys_configsServer _sysconfigsserver;
        private readonly Igoods_catsServer _goodscatsserver;
        private readonly IgoodServer _goodserver;


        public IndexModel(Igoods_catsServer goodscatsserver, Isys_configsServer sysconfigsserver, IgoodServer goodserver)
        {
            _goodscatsserver = goodscatsserver;
            _sysconfigsserver = sysconfigsserver;
            _goodserver = goodserver;
        }


        public List<goods_catsViewModel> goods_cats_top1 { get; set; }
        public List<goods_catsViewModel> goods_cats { get; set; }
        public List<goodsViewModel> goods { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }
        public long GoodsCount { get; set; }
        public string CatName { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["Title"] = "产品类别 ";
            ViewData["keywords"] = "产品类别 ";

            ViewData["keywords"] = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");

            var post = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 30, attr = 0, where = "parentId" });
            goods_cats_top1 = post.data.Items.MapTo<List<goods_catsViewModel>>();

            var post2 = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 200 });
            goods_cats = post2.data.Items.MapTo<List<goods_catsViewModel>>();


            if (int.TryParse(ID, out int _ID))
            {
                var postgoods = await _goodserver.GetPagesAsync(new PageParm() { limit = 20, attr = _ID, where = "goodsCatId" });
                goods = postgoods.data.Items.MapTo<List<goodsViewModel>>();
                GoodsCount = postgoods.data.TotalItems;
               if (postgoods.data.Items.Count>0) CatName = postgoods.data.Items[0].catName;
            }

           


        }

       
    }
}
