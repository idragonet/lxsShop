﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using FineUICore;
using lxsShop.Common.ImgHelper;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.Repository;
using lxsShop.Services;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Masuit.Tools.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DbFactory = lxsShop.NewServices.DbFactory;
using Image = System.Drawing.Image;

namespace lxsShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsController : lxsShop.Web.Controllers.BaseController
    {
        private readonly IgoodServer _goodserver;
        private readonly Igoods_catsServer _goodscatsserver;
        private readonly IbrandsServer _brandsserver;

        public GoodsController(IgoodServer goodserver, Igoods_catsServer goodscatsserver, IbrandsServer brandsserver)
        {
            _goodserver = goodserver;
            _goodscatsserver = goodscatsserver;
            _brandsserver = brandsserver;
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search_Click(string[] Grid1_fields,string searchKey)
        {
            var grid1 = UIHelper.Grid("Grid1");

            var parm = new PageParm
            {
                page = 1,
                limit = 50,
            };

            if (!string.IsNullOrEmpty(searchKey))
            {
                parm = new PageParm
                {
                    page = 1,
                    limit = 50,
                    key = searchKey,
                    where = "search"
                };
            }


            var post = await _goodserver.GetPagesAsync(
                parm
            );

            var recordCount = Convert.ToInt32(post.data.TotalItems);

            // 1.设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);

            grid1.DataSource(post.data.Items, Grid1_fields);

            return UIHelper.Result();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search2_Click(string[] Grid1_fields, string searchKey)
        {
            var grid1 = UIHelper.Grid("Grid1");

            var parm = new PageParm
            {
                page = 1,
                limit = 50,
            };

            if (!string.IsNullOrEmpty(searchKey))
            {
                parm = new PageParm
                {
                    page = 1,
                    limit = 500,
                    key = searchKey,
                    where = "search2"
                };
            }


            var post = await _goodserver.GetPagesAsync(
                parm
            );

            var recordCount = Convert.ToInt32(post.data.TotalItems);

            // 1.设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);

            grid1.DataSource(post.data.Items, Grid1_fields);

            return UIHelper.Result();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModClass_Click(JArray selected,long catid)
        {
            var goodsList = new List<long>();
            foreach (JArray item in selected)
            {
                goodsList.Add(Convert.ToInt64(item[0]));

                /*sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                    item[0], item[1],
                    Convert.ToInt32(item[2].ToString()) == 1 ? "男" : "女",
                    item[3]);*/
            }

            if (selected.Count == 0)
            {
                ShowNotify("没有选择行.", MessageBoxIcon.Warning);
            }
            if (catid<= 0)
            {
                ShowNotify("请选择正确类别.", MessageBoxIcon.Warning);
            }

            var post = await _goodserver.ModifyCatAsync(catid, goodsList);

            if (post.success)
            {
                ShowNotify(post.message, MessageBoxIcon.Information);
            }
            else
            {
                ShowNotify("更新失败.", MessageBoxIcon.Warning);
            }

            /*
            if (!string.IsNullOrEmpty(searchKey))
            {
                parm = new PageParm
                {
                    page = 1,
                    limit = 500,
                    key = searchKey,
                    where = "search2"
                };
            }


            var post = await _goodserver.GetPagesAsync(
                parm
            );

            var recordCount = Convert.ToInt32(post.data.TotalItems);

            // 1.设置总项数（数据库分页回发时，如果总记录数不变，可以不设置RecordCount）
            grid1.RecordCount(recordCount);

            grid1.DataSource(post.data.Items, Grid1_fields);
            */

            return UIHelper.Result();
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
            var postcat = await _goodscatsserver.GetPagesAsync(new PageParm() { limit = 10000 });
            var result = postcat.data.Items.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();

            var root = new goods_catsViewModel { parentId = -1, catId = 0, catName = "--根节点--" };
            result.Add(root);

            ResolveCollection(result, null, -1, 0);
            ViewBag.goods_catsDataSource = _resultNew;


            request.limit = 50;
          //  request.order = "DESC";
            request.field = "ordering DESC,CreateDate DESC";

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
        public async Task<IActionResult> Grid1_PageIndexChangedOrSort(string[] Grid1_fields, int Grid1_pageIndex,
            string Grid1_sortField, string Grid1_sortDirection, string searchKey)
        {
            var grid1 = UIHelper.Grid("Grid1");

            var parm = new PageParm
            {
                page = (Grid1_pageIndex + 1),
                limit = 50,
                order = Grid1_sortDirection,
                field = Grid1_sortField
            };

            if (!string.IsNullOrEmpty(searchKey))
            {
                parm = new PageParm
                {
                    page = (Grid1_pageIndex + 1),
                    limit = 50,
                    order = Grid1_sortDirection,
                    field = Grid1_sortField,
                    key = searchKey,
                    where = "search"
                };
            }


            var post = await _goodserver.GetPagesAsync(
                parm
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
            }else if (actionType == "search")
            {
                await _goodserver.DeleteAsync(deletedRowID.ToString());
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


        #region 新增

        private static List<goods_catsViewModel> _resultNew;


        private static int ResolveCollection(List<goods_catsViewModel> result, goods_catsViewModel parentMenu,
            long parentId, int level)
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
            var post = await _goodscatsserver.GetPagesAsync(new PageParm() {limit = 10000 });
            var result = post.data.Items.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();

            var root = new goods_catsViewModel {parentId = -1, catId = 0, catName = "--根节点--"};
            result.Add(root);

            ResolveCollection(result, null, -1, 0);
            ViewBag.goods_catsDataSource = _resultNew;


            var post2 = await _brandsserver.GetPagesAsync(new PageParm()
                {limit = 500, field = "brandName", order = "ASC"});
            var brandsNA = new brands {brandId = -1, brandName = "N/A"};
            post2.data.Items.Insert(0, brandsNA);
            ViewBag.brands_DataSource = post2.data.Items;

            //ViewBag.DataSource = _goodserver.GetPagesAsync(new PageParm()).Result.data.Items;
            return View();
        }


       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New_btnSaveClose_Click(goodsViewModel goodsview, IFormFile filePhoto,
            string text)
        {
            var goods = new goods();

            if (ModelState.IsValid)
            {
                if (filePhoto != null)
                {
                    var fileName = filePhoto.FileName;

                    if (!ValidateFileType(fileName))
                    {
                        // 清空文件上传组件
                        UIHelper.FileUpload("filePhoto").Reset();

                        //   ShowNotify("无效的文件类型！");
                        return UIHelper.Result();
                    }
                    else
                    {

                        goods.goodsImg = UtilsShop.SavePic(filePhoto, PageContext.MapWebPath("~/uploads/"));

                        /*string fileExt = Path.GetExtension(filePhoto.FileName);
                        string fileDir = Path.Combine(PageContext.MapWebPath("~/uploads/"),
                            DateTime.Now.ToString("yyyyMM"));
                        if (!Directory.Exists(fileDir))
                        {
                            Directory.CreateDirectory(fileDir);
                        }

                        string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                        string newFileName300 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_300.jpg";
                        string newFileName100 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_100.jpg";
                        string filePath = Path.Combine(fileDir, newFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            filePhoto.CopyTo(stream);
                        }


                        new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName300), 300, 300);
                        new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName100), 100, 100);

                        goods.goodsImg = "/" + DateTime.Now.ToString("yyyyMM") + "/" + newFileName;*/
                    }
                }

                goods.CreateDate = DateTime.Now;
                if (goodsview.brandId != null) goods.brandId = Convert.ToInt64(goodsview.brandId);
                goods.isNew = Convert.ToInt32(goodsview.isNew);
                goods.isRecom = Convert.ToInt32(goodsview.isRecom);
                goods.goodsCatId = goodsview.goodsCatId;
                goods.goodsSeoKeywords = goodsview.goodsSeoKeywords;
                goods.goodsSn = goodsview.goodsSn;
                goods.goodsName = goodsview.goodsName;
                goods.goodsDesc = text;
                goods.ordering = goodsview.goodsId;




                var ret = await _goodserver.AddAsync(goods);

                if (ret.statusCode == 200)
                {
                    ActiveWindow.HidePostBack();
                }
                else
                {
                    return Ok(ret);
                }

                //  await _goodserver.AddAsync(goods);
                //  return Ok(await _goodserver.AddAsync(goods));
                // 关闭本窗体（触发窗体的关闭事件）
                //   ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }

        #endregion




        #region 编辑


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _goodscatsserver.GetPagesAsync(new PageParm { limit = 10000 });
            var result = post.data.Items.MapTo<List<goods_catsViewModel>>();


            _resultNew = new List<goods_catsViewModel>();

            var root = new goods_catsViewModel { parentId = -1, catId = 0, catName = "--根节点--" };
            result.Add(root);

            ResolveCollection(result, null, -1, 0);
            ViewBag.goods_catsDataSource = _resultNew;


            var post2 = await _brandsserver.GetPagesAsync(new PageParm()
                { limit = 500, field = "brandName", order = "ASC" });
            var brandsNA = new brands { brandId = -1, brandName = "N/A" };
            post2.data.Items.Insert(0, brandsNA);
            ViewBag.brands_DataSource = post2.data.Items;


            var good = _goodserver.GetPagesAsync(new PageParm {id = id});
            if (good.Result.data.Items.Count==0) return Content("无效参数！");

            ViewBag.goodsDesc = good.Result.data.Items[0].goodsDesc;

            return View(good.Result.data.Items[0]);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_btnSaveClose_Click(goodsViewModel goodsview, IFormFile filePhoto,
            string text)
        {
            var goods = new goods();

            if (ModelState.IsValid)
            {
                if (filePhoto != null)
                {
                    var fileName = filePhoto.FileName;

                    if (!ValidateFileType(fileName))
                    {
                        // 清空文件上传组件
                        UIHelper.FileUpload("filePhoto").Reset();

                        //   ShowNotify("无效的文件类型！");
                        return UIHelper.Result();
                    }
                    else
                    {
                        string fileExt = Path.GetExtension(filePhoto.FileName);
                        string fileDir = Path.Combine(PageContext.MapWebPath("~/uploads/"),
                            DateTime.Now.ToString("yyyyMM"));
                        if (!Directory.Exists(fileDir))
                        {
                            Directory.CreateDirectory(fileDir);
                        }

                        string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                        string newFileName300 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_300.jpg";
                        string newFileName100 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_100.jpg";
                        string filePath = Path.Combine(fileDir, newFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            filePhoto.CopyTo(stream);
                        }


                        new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName300), 300, 300);
                        new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName100), 100, 100);

                        goods.goodsImg = "/" + DateTime.Now.ToString("yyyyMM") + "/" + newFileName;
                    }
                }


                if (filePhoto == null) goods.goodsImg = goodsview.goodsImg;

                    goods.CreateDate = DateTime.Now;
                if (goodsview.brandId != null) goods.brandId = Convert.ToInt64(goodsview.brandId);
                goods.isNew = Convert.ToInt32(goodsview.isNew);
                goods.isRecom = Convert.ToInt32(goodsview.isRecom);

                goods.goodsCatId = goodsview.goodsCatId;
                goods.goodsSeoKeywords = goodsview.goodsSeoKeywords;
                goods.goodsSn = goodsview.goodsSn;
                goods.goodsName = goodsview.goodsName;
                goods.goodsDesc = text;
                goods.goodsId = goodsview.goodsId;
                goods.ordering = goodsview.ordering;


                var ret = await _goodserver.ModifyAsync(goods);

                if (ret.statusCode == 200)
                {
                   // ActiveWindow.HidePostBack();
                    ActiveWindow.Hide();
                }
                else
                {
                    return Ok(ret);
                }

                //  await _goodserver.AddAsync(goods);
                //  return Ok(await _goodserver.AddAsync(goods));
                // 关闭本窗体（触发窗体的关闭事件）
                //   ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }

        #endregion


    }
}