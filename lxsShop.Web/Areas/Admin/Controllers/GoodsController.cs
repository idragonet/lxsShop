using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using FineUICore;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.Repository;
using lxsShop.Services;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbFactory = lxsShop.NewServices.DbFactory;

namespace lxsShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class GoodsController : Controller
    {

        private readonly IgoodServer _goodserver;
        private readonly Igoods_catsServer _goodscatsserver;

        public GoodsController(IgoodServer goodserver, Igoods_catsServer goodscatsserver)
        {
            _goodserver = goodserver;
            _goodscatsserver = goodscatsserver;
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
                //该品牌的的商品必须没有才允许删除
                /*var goods = goods_service.FindByClause(m => m.brandId == deletedRowID.Value);
                if (goods != null)
                {
                    Alert.ShowInTop("删除失败！需要先清空该品牌下的商品");
                    return UIHelper.Result();
                }*/
                await _goodserver.DeleteAsync(deletedRowID.ToString());
                //  brandsservice.DeleteById(deletedRowID);
            }

            return RedirectToAction("Index");

            // return UIHelper.Result();
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





        #region 品牌 新增

        private static List<goods_catsViewModel> _resultNew;


        private static int ResolveCollection(List<goods_catsViewModel> result, goods_catsViewModel parentMenu, long parentId, int level)
        {
            int count = 0;

            foreach (var goodsCatsViewModel in result.Where(m => m.parentId == parentId))
            {
                count++;
                goodsCatsViewModel.TreeLevel = level;
                _resultNew.Add(goodsCatsViewModel);

                level++;
                ResolveCollection(result, goodsCatsViewModel, goodsCatsViewModel.catId, level);
                level--;
            }

            return count;
        }


        [Authorize]
        public async Task<IActionResult> New()
        {
            var post = await _goodscatsserver.GetPagesAsync(new PageParm() {limit = 100});
            var result = post.data.Items.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();

            var root = new goods_catsViewModel {parentId = -1, catId = 0, catName = "--根节点--"};
            result.Add(root);

            ResolveCollection(result, null, -1, 0);
            ViewBag.goods_catsDataSource = _resultNew;


            ViewBag.DataSource = _goodserver.GetPagesAsync(new PageParm()).Result.data.Items;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New_btnSaveClose_Click(
            [Bind(include:"brandId,brandName")]
            brands brandsEdit, IFormFile filePhoto)
        {
            if (ModelState.IsValid)
            {
                if (filePhoto != null)
                {
                    var fileName = filePhoto.FileName;

                    /*if (!ValidateFileType(fileName))
                    {
                        // 清空文件上传组件
                        UIHelper.FileUpload("filePhoto").Reset();

                     //   ShowNotify("无效的文件类型！");
                        return UIHelper.Result();
                    }
                    else
                    {
                        fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                        fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                        brandsEdit.brandImg = fileName;

                        using (var stream = new FileStream(PageContext.MapWebPath("~/uploads/Logo/" + fileName),
                            FileMode.Create))
                        {
                            filePhoto.CopyTo(stream);
                        }
                    }*/
                }

                brandsEdit.CreateDate = DateTime.Now;


             //   brandsservice.Insert(brandsEdit);

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }



        #endregion





    }
}
