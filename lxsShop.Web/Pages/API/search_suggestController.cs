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

        private readonly IgoodServer _goodserver;

        //   private readonly IBbs_QuestionsService _askService;
        public search_suggestController( IgoodServer goodserver)
        {
            _goodserver = goodserver;
            //   _askService = askService;
        }

        [HttpPost]
        public async Task<ActionResult> search_suggest([FromForm] string keyword)
        {
             
            
            // [FromForm] string keyword, 
             var postgoods = await _goodserver.GetPagesAsync(new PageParm()
             {
                 key = keyword, 
                 where = "search"
             });

            string message = string.Format("搜索[{0}]找到{1}个产品", keyword, postgoods.data.TotalItems);

            var t1 = new SearchSuggest() { href = "/search/"+ keyword, name = message };
            return new JsonResult(new List<SearchSuggest> { t1 });
        }

        public class Avatar
        {
            public string keyword { get; set; }
        }

    }
}
