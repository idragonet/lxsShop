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
            request.limit = 20;
            request.order = "DESC";
            request.field = "CreateDate";

            ViewBag.Grid1SortField = request.field;
            ViewBag.Grid1SortDirection = request.order;

            var post = await _goodserver.GetPagesAsync(request);

            ViewBag.Grid1RecordCount = Convert.ToInt32(post.data.TotalItems); 
            return View(post.data.Items);
              // return View(post.data);
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Grid1_PageIndexChanged(string[] Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");


            var post = await _goodserver.GetPagesAsync(
                new PageParm{page = (Grid1_pageIndex+1),limit = 20 }
                );

            var recordCount = Convert.ToInt32(post.data.TotalItems);

            // 1.设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);

            // 2.获取当前分页数据
         
            grid1.DataSource(post.data.Items, Grid1_fields);

            return UIHelper.Result();
        }*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Grid1_PageIndexChangedOrSort(string[] Grid1_fields, int Grid1_pageIndex, string Grid1_sortField, string Grid1_sortDirection)
        {
            var grid1 = UIHelper.Grid("Grid1");

            var post = await _goodserver.GetPagesAsync(
                new PageParm { page = (Grid1_pageIndex + 1), limit = 20,order = Grid1_sortDirection , field = Grid1_sortField }
            );

            var recordCount = Convert.ToInt32(post.data.TotalItems);

            // 1.设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);

            grid1.DataSource(post.data.Items, Grid1_fields);

            return UIHelper.Result();
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
