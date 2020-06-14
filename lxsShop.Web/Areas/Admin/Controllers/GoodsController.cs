using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineUICore;
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

        public IgoodsService _goodsservice { get; }

        //通过构造函数注入Service
        public GoodsController(IgoodsService goodsservice)
        {
            _goodsservice = goodsservice;
        }


        #region 显示、删除


      

        [Authorize]
        public IActionResult Index()
        {
            var post = _goodsservice.FindAll();
            var result = post.MapTo<List<goodsViewModel>>();
            return View(result);
        }



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
        }

        #endregion


    }
}
