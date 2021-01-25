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
     //   private readonly IBbs_QuestionsService _askService;
        public IndexModel(IbrandsServer brandsServer
          )
        {
            _brandsServer = brandsServer;
         //   _askService = askService;
        }

        public List<brands> brandsList { get; set; }


        /// <summary>
        /// 问题列表
        /// </summary>
       // public Page<Bbs_Questions> Ask { get; set; }

        public void OnGet(string category = null, string key = null, string where = "", int limit = 5)
        {
            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }
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
