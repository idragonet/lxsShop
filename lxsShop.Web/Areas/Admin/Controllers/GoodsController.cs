using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineUICore;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.Repository;
using lxsShop.Services;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class GoodsController : Controller
    {

        private readonly IgoodServer _goodserver;

        public GoodsController(IgoodServer goodserver)
        {
            _goodserver = goodserver;
        }


        #region 显示、删除


        /*[HttpGet]
        public async Task<IActionResult> Get()
        {
            var info = await services.GetCategory("31d5f34b-e0cc-4e47-aa9a-75c4890b709d");
            return Ok(info);
        }*/


        [Authorize]
        public async Task<IActionResult> Index([FromQuery] PageParm request)
        {
            var post = await _goodserver.GetPagesAsync(request);

            return View(post.data.Items);
        }



        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brands_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
                _goodsservice.DeleteById(deletedRowID);
            }

            var post = _goodsservice.FindAll();
            var result = post.MapTo<List<goodsViewModel>>();
            UIHelper.Grid("Grid1").DataSource(result, Grid1_fields);

            return UIHelper.Result();
        }*/

        #endregion


    }
}
