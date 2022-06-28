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
        private readonly IbrandsServer _brandsserver;

        public IndexModel(Igoods_catsServer goodscatsserver, Isys_configsServer sysconfigsserver,
            IgoodServer goodserver, IbrandsServer brandsserver)
        {
            _goodscatsserver = goodscatsserver;
            _sysconfigsserver = sysconfigsserver;
            _goodserver = goodserver;
            _brandsserver = brandsserver;
        }


        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public string TopcatName { get; set; }
        public string brandImg { get; set; }

        public goodsViewModel  goods { get; set; }

        public async Task OnGetAsync()
        {
            var post = await _goodserver.GetPagesAsync(new PageParm() {id = ID});

            ViewData["Title"] = "没有找到该产品";
            ViewData["keywords"] = "产品类别 ";
            ViewData["description"] = await _sysconfigsserver.GetKeyAsync("description");
            ViewData["Tel"] = await _sysconfigsserver.GetKeyAsync("tel");

            

            if (post.data.Items.Count > 0)
            {
                ViewData["Title"] = post.data.Items[0].goodsName + " - " + post.data.Items[0].catName + " - " + await _sysconfigsserver.GetKeyAsync("Title");
                goods = post.data.Items[0];

                //通过商品归属二级类别ID获取到一级类别ID
                var postcat = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 1, attr =Convert.ToInt32(post.data.Items[0].goodsCatId), where = "catid" });
                if (postcat.data.Items.Count > 0)
                {
                    var parentId = postcat.data.Items[0].parentId;
                    postcat = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 1, attr = Convert.ToInt32(parentId), where = "catid" });
                    if (postcat.data.Items.Count > 0)
                    {
                        TopcatName= postcat.data.Items[0].catName;
                    }
                }

                if (goods.brandId > 0)
                {
                   var brand = await _brandsserver.GetPagesAsync(new PageParm()
                        { limit = 100,  key = goods.brandId.ToString() });
                   if (brand.data.Items.Count > 0)
                   {
                       brandImg = "/uploads/Logo/" + brand.data.Items[0].brandImg;
                   }
                }


            }

           
        }
    }
}
