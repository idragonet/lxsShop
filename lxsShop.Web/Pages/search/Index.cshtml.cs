using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using HtmlAgilityPack;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Masuit.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.search
{
    public class IndexModel : PageModel
    {
        private readonly Isys_configsServer _sysconfigsserver;
        private readonly Igoods_catsServer _goodscatsserver;
        private readonly IgoodServer _goodserver;


        public IndexModel(Igoods_catsServer goodscatsserver, Isys_configsServer sysconfigsserver,
            IgoodServer goodserver)
        {
            _goodscatsserver = goodscatsserver;
            _sysconfigsserver = sysconfigsserver;
            _goodserver = goodserver;
        }


        public List<goods_catsViewModel> goods_cats_top1 { get; set; }
        public List<goods_catsViewModel> goods_cats { get; set; }


        public List<goods_cats> goodscatsLeft { get; set; }




        public List<goodsViewModel> goods { get; set; }

        [BindProperty(SupportsGet = true)] public string keyword { get; set; }
       // [BindProperty(SupportsGet = true)] public int? pages { get; set; }

        public string GoodsCount { get; set; }
        public long TotalPages { get; set; }
        public long CurrentPage { get; set; }


        [FromQuery(Name = "catid")]
        public int CatID { get; set; }

        [FromQuery(Name = "p")]
        public int? pages { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            if (pages == null) pages = 1;

            if (IsAjax(HttpContext.Request))
            {
                var web = new HtmlWeb();
                var doc = web.Load(Request.GetDisplayUrl());
                var titleNode = doc.DocumentNode.SelectSingleNode("//div[@id='products_list']");
                return Content(titleNode.InnerHtml);
            }

            ViewData["Title"] = string.Format("搜索'{0}'相关产品", keyword);
            
            ViewData["keywords"] = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");

            ViewData["Title"] = string.Format("搜索'{0}'相关产品", keyword) + " - " + await _sysconfigsserver.GetKeyAsync("Title");



            var post = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 20, attr = 0, where = "parentId" });
            goods_cats_top1 = AutoMapperHelper.MapTo<List<goods_catsViewModel>>(post.data.Items);

            var post2 = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 200 });
            goods_cats = AutoMapperHelper.MapTo<List<goods_catsViewModel>>(post2.data.Items);

            
            var postgoods = await _goodserver.GetPagesAsync(new PageParm()
            {
                limit = 16, page = Convert.ToInt16(pages), key   = keyword, where = "search" 
                ,attr = CatID
            });
            goods = AutoMapperHelper.MapTo<List<goodsViewModel>>(postgoods.data.Items);

            GoodsCount = $"(共{postgoods.data.TotalItems}个产品)";
            TotalPages = postgoods.data.TotalPages;
            CurrentPage = postgoods.data.CurrentPage;


            //读取搜索找到的类别
            var goodscat = postgoods.data.Items.DistinctBy(c => c.goodsCatId).ToList();
            var goodscaID= goodscat.Select(c => c.goodsCatId).ToList();


            //  .DistinctBy(c => c.Email).ToList();
            var post_getgoodscat = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 100,IdList  = goodscaID, where = "getgoodscat" });
            goodscatsLeft = post_getgoodscat.data.Items;




            return null;
        }

        public static bool IsAjax(HttpRequest req)
        {
            bool result = false;
            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }




    }
}
