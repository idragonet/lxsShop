using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Views.Home
{
    public class IndexModel : PageModel
    {

        private readonly IbrandsServer _brandsServer;
        private readonly Isys_configsServer _sysconfigsserver;
        private readonly IbannerServer _bannerserver;
        //   private readonly IBbs_QuestionsService _askService;
        public IndexModel(IbrandsServer brandsServer, Isys_configsServer sysconfigsserver, IbannerServer bannerserver)
        {
            _brandsServer = brandsServer;
            _sysconfigsserver = sysconfigsserver;
            _bannerserver = bannerserver;
            //   _askService = askService;
        }

        public List<brands> brandsList { get; set; }
        public List<banner> bannerList { get; set; }


        /// <summary>
        /// 问题列表
        /// </summary>
       // public Page<Bbs_Questions> Ask { get; set; }

        public async Task OnGetAsync(string category = null, string key = null, string where = "", int limit = 5)
        {
            ViewData["Title"] = await _sysconfigsserver.GetKeyAsync("Title");
            ViewData["Tel"] = await _sysconfigsserver.GetKeyAsync("tel");
            ViewData["keywords"] = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewData["Copyright"] = await _sysconfigsserver.GetKeyAsync("Copyright");
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");
            ViewData["首页"] = await _sysconfigsserver.GetKeyAsync("首页");
            ViewData["报价"] = await _sysconfigsserver.GetKeyAsync("报价");
            ViewData["热门搜索"] = await _sysconfigsserver.GetKeyAsync("热门搜索");

            ViewData["banner_bg"] = "\"#EFF8FF\",\"#468BC1\",\"#79E047\"";

            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }

            // var brandsPost = await _brandsServer.GetPagesAsync(new PageParm {page = 22});
            var brandsPost = await _brandsServer.GetPagesAsync(new PageParm()
            { limit = 100, field = "brandName", order = "ASC" });
            brandsList = brandsPost.data.Items;




            var bannerpost = await _bannerserver.GetPagesAsync(new PageParm{order = "DESC", field = "BannerOrder" });
            bannerList = bannerpost.data.Items;

            string banner_bg="";
            foreach (var banner in bannerList)
            {
                banner_bg += string.IsNullOrEmpty(banner.BackgroundColor)
                    ? "\"rgb(239, 248, 255)\","
                    : "\"" + banner.BackgroundColor + "\",";
            }
            if (banner_bg.Length > 0) banner_bg = banner_bg.Substring(0, banner_bg.Length - 1);
            ViewData["banner_bg"] = banner_bg;

            /*Types = where;
            pageIndex = page;
            Limit = limit;

            classifyList = _classifyService.GetListAsync(m => !m.IsDel, m => m.FirstLetter, DbOrderEnum.Asc).Result.data;
            if (category != null)
            {
                Classify = category;
                category = classifyList.FirstOrDefault(m => m.EnClassName == category)?.Guid;
            }
            var param = new PageParm() { guid = category, page = page, key = key, audit = 1, where = where, limit = limit };
            Ask = _askService.GetPageList(param).Result.data;*/
        }
    }
}
