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

        //   private readonly IBbs_QuestionsService _askService;
        public IndexModel(IbrandsServer brandsServer, Isys_configsServer sysconfigsserver)
        {
            _brandsServer = brandsServer;
            _sysconfigsserver = sysconfigsserver;
            //   _askService = askService;
        }

        public List<brands> brandsList { get; set; }


        /// <summary>
        /// 问题列表
        /// </summary>
       // public Page<Bbs_Questions> Ask { get; set; }

        public async Task OnGetAsync(string category = null, string key = null, string where = "", int limit = 5)
        {
            ViewData["Title"] = await _sysconfigsserver.GetKeyAsync("Title");
            ViewData["Tel"] = await _sysconfigsserver.GetKeyAsync("手机号码");
            ViewData["keywords"] = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewData["Copyright"] = await _sysconfigsserver.GetKeyAsync("Copyright");
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");

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
