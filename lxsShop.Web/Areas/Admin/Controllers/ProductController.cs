using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class ProductController : BaseController
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Category()
        {
            var postRepository = new CategoryRepository();
            var post = postRepository.FindById(1);
            return View(post);
        
        }


        [Authorize]
        public IActionResult Brand()
        {
            return View();
        }


        public IActionResult Dept_DoPostBack()
        {
            throw new NotImplementedException();
        }
    }
}