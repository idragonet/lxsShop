using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.fa
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


        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        public goodsViewModel  goods { get; set; }

        public async Task OnGetAsync()
        {
            var post = await _goodserver.GetPagesAsync(new PageParm() {id = ID});

            ViewData["Title"] = "没有找到该产品";
            ViewData["keywords"] = "产品类别 ";
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");


            if (post.data.Items.Count > 0)
            {
                ViewData["Title"] = post.data.Items[0].goodsName + " - " + post.data.Items[0].catName + " - " + await _sysconfigsserver.GetKeyAsync("Title");
                goods = post.data.Items[0];
            }

           
        }
    }
}
