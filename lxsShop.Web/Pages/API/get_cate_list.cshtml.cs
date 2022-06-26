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

namespace lxsShop.Web.Pages.API
{
    public class get_cate_listModel : PageModel
    {
        private readonly Igoods_catsServer _goodscatsserver;


        public get_cate_listModel(Igoods_catsServer goodscatsserver)
        {
            _goodscatsserver = goodscatsserver;
        }


        public List<goods_catsViewModel> goods_cats_top1 { get; set; }
        public List<goods_catsViewModel> goods_cats { get; set; }

        public async Task OnGetAsync()
        {
            var post = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 14,attr =0,where = "parentId" });
            goods_cats_top1 = post.data.Items.MapTo<List<goods_catsViewModel>>().OrderByDescending(x=>x.catSort).ToList();

            var post2 = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 180});
            goods_cats = post2.data.Items.MapTo<List<goods_catsViewModel>>().OrderByDescending(x => x.catSort).ToList();

        }
    }
}
