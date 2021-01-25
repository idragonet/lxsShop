using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lxsShop.NewServices.Interfaces;

namespace lxsShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Isys_configsServer _sysconfigsserver;


        public HomeController(Isys_configsServer sysconfigsserver)
        {
            _sysconfigsserver = sysconfigsserver;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = await _sysconfigsserver.GetKeyAsync("Title");
            ViewBag.Tel = await _sysconfigsserver.GetKeyAsync("手机号码");
            ViewBag.keywords = await _sysconfigsserver.GetKeyAsync("keywords");
            ViewBag.Copyright = await _sysconfigsserver.GetKeyAsync("Copyright");
            ViewBag.description = await _sysconfigsserver.GetKeyAsync("description");

            return View();
        }
    }
}
