using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Web.Pages.API
{
    public class search_suggestModel : PageModel
    {

        [FromQuery(Name = "keyword")]
        public string keyword { get; set; }

        public async Task OnPostAsync()
        {



        }
    }
}
