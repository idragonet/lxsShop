using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.article
{
    public class IndexModel : PageModel
    {
      
        private readonly Isys_configsServer _sysconfigsserver;
        private readonly IgoodServer _goodserver;
        private readonly IarticlesServer _articlesServer;
        //   private readonly IBbs_QuestionsService _askService;
        public IndexModel(Isys_configsServer sysconfigsserver, IarticlesServer articlesServer, IgoodServer goodserver)
        {
            _sysconfigsserver = sysconfigsserver;

            _articlesServer = articlesServer;
            _goodserver = goodserver;
            //   _askService = askService;
        }

        public List<brands> brandsList { get; set; }
        public List<banner> bannerList { get; set; }
        public List<goodsViewModel> goodsList_cat2 { get; set; }
        public List<goodsViewModel> goodsList_cat3 { get; set; }


        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }
        public articlesViewModel _articles { get; set; }

        /// <summary>
        /// 问题列表
        /// </summary>
        // public Page<Bbs_Questions> Ask { get; set; }

        public async Task OnGetAsync(string category = null, string key = null, string where = "", int limit = 5)
        {

            if (string.IsNullOrEmpty(ID) || !long.TryParse(ID,out long result))
            {
                ViewData["Title"] = "没有找到文章 - " + await _sysconfigsserver.GetKeyAsync("Title");
            }
            else
            {
                var post = await _articlesServer.GetPagesAsync(new PageParm() {key = ID});
                if (post.data.Items.Count == 0)
                {
                    ViewData["Title"] = "没有找到文章 - " + await _sysconfigsserver.GetKeyAsync("Title");
                }
                else
                {
                    ViewData["Title"] = post.data.Items[0].articleTitle+ " - " + await _sysconfigsserver.GetKeyAsync("Title");
                    _articles = post.data.Items[0];

                    ViewData["Title2"] = post.data.Items[0].articleTitle;
                }
            }


            




        }
    }
}
