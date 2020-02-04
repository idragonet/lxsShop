using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.Repository;
using lxsShop.Services;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class ProductController : BaseController
    {
        public ICategoryService categoryservice { get; }
        //通过构造函数注入Service
        public ProductController(ICategoryService categoryservice)
        {
            this.categoryservice = categoryservice;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Category()
        {
            /*var postRepository = new CategoryRepository();
            var post = postRepository.FindById(1);*/

           
            LogManager.Info("记录一次消息");

            var post = categoryservice.FindById(1);
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