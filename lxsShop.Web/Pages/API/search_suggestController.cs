using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;

namespace lxsShop.Web.Pages.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class search_suggestController : ControllerBase
    {

        private  IgoodServer _goodserver;



        [HttpPost]
        public async Task<ActionResult> search_suggest([FromForm] string keyword)
        {
            // _goodserver = goodserver;
            //
            // var postgoods = await _goodserver.GetPagesAsync(new PageParm()
            // {
            //     key = keyword,
            //     where = "search"
            // });

            string message = string.Format("搜索[{0}]找到{1}个产品", keyword,15);

            var t1 = new SearchSuggest() { href = "/search/"+ keyword, name = message };
            return new JsonResult(new List<SearchSuggest> { t1 });
        }

 

    }
}
