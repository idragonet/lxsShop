using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lxsShop.Web.Pages.catalog
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

        public List<goods_catsViewModel> list_class3 { get; set; } //三级分类

        public List<goodsViewModel> goods { get; set; }

        [BindProperty(SupportsGet = true)] public int ID { get; set; }


        [BindProperty(SupportsGet = true)] public int? pages { get; set; }

        public long GoodsCount { get; set; }
        public long TotalPages { get; set; }
        public long CurrentPage { get; set; }
        public string CatName { get; set; }
        public string 纸质产品目录 { get; set; }

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

            ViewData["Title"] = "产品类别 ";
            ViewData["keywords"] = "产品类别 ";

            ViewData["keywords"] = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");

            纸质产品目录 = await _sysconfigsserver.GetKeyAsync("纸质产品目录");

            var post = await _goodscatsserver.GetPagesAsync(new PageParm() {limit = 40, attr = 0, where = "parentId"});
            goods_cats_top1 = post.data.Items.MapTo<List<goods_catsViewModel>>().OrderByDescending(x => x.catSort)
                .ToList();
            //.OrderByDescending(x=>x.catSort).ToList();

            var post2 = await _goodscatsserver.GetPagesAsync(new PageParm() {limit = 300});
            goods_cats = post2.data.Items.MapTo<List<goods_catsViewModel>>().OrderByDescending(x => x.catSort).ToList();


            //class3
            var class3 =
                await _goodscatsserver.GetPagesAsync(new PageParm() {limit = 300, attr = ID, where = "class3"});
            if (class3 != null && class3.data != null)
            {
                list_class3 = class3.data.Items.MapTo<List<goods_catsViewModel>>().OrderByDescending(x => x.catSort)
                    .ToList();
            }

            


            //当前类别是一级的时候读取商品
            var postgoods = new ApiResult<Page<goodsViewModel>>();
            var catList = goods_cats.Where(t => t.catId == ID && t.parentId == 0).ToList();
            if (catList.Count > 0)
            {
                CatName = catList[0].catName;
                catList = goods_cats.Where(t => t.parentId == ID).ToList();
                var catLong = catList.Select(t => t.catId).ToList();//加载二级分类ID

                for (int i = 0; i < catLong.Count; i++)//加载三级分类ID
                {
                    var cat3List = goods_cats.Where(t => t.parentId == catLong[i] ).ToList();
                    catLong.AddRange(cat3List.Select(t => t.catId).ToList());
                }

                postgoods = await _goodserver.GetPagesAsync(new PageParm()
                {
                    limit = 16, page = Convert.ToInt16(pages), IdList = catLong, field = "ordering DESC,CreateDate DESC"
                });
            }
            else //当前类别是二级、三级的时候读取商品
            {
                var cat3List2 = goods_cats.Where(t => t.parentId == ID).ToList();
                var catLong2 = cat3List2.Select(t => t.catId).ToList();
                catLong2.Add(ID);
                 postgoods = await _goodserver.GetPagesAsync(new PageParm()
                {
                    limit = 16,
                    page = Convert.ToInt16(pages),
                    IdList = catLong2,
                    field = "ordering DESC,CreateDate DESC"
                });

                catList = goods_cats.Where(t => t.catId == ID).ToList();
                CatName = catList[0].catName;
            }

            goods = postgoods.data.Items.MapTo<List<goodsViewModel>>();
            GoodsCount = postgoods.data.TotalItems;
            TotalPages = postgoods.data.TotalPages;
            CurrentPage = postgoods.data.CurrentPage;
            ViewData["Title"] = CatName + " - " + await _sysconfigsserver.GetKeyAsync("Title");

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