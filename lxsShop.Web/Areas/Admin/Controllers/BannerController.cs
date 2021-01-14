using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using FineUICore;
using lxsShop.Common.ImgHelper;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace lxsShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : lxsShop.Web.Controllers.BaseController
    {

        private readonly IbannerServer _bannerserver;

        public BannerController(IbannerServer bannerserver)
        {
            _bannerserver = bannerserver;
        }


        #region 显示、删除


        [Authorize]
        public async Task<IActionResult> Index([FromQuery] PageParm request)
        {
            request.limit = 20;
            request.order = "DESC";
            request.field = "CreateDate";
            
            var post = await _bannerserver.GetPagesAsync(request);

            /*ViewBag.Grid1RecordCount = Convert.ToInt32(post.data.TotalItems);
            return View(post.data.Items);*/


            var result = post.data.Items.MapTo<List<bannerViewModel>>();
            return View(result);

            // return View(post.data);
        }
 


        //   public brandsRepository BrandsRepository = new brandsRepository();

        //  public goodsRepository goods_service = new goodsRepository();
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
                await _bannerserver.DeleteAsync(deletedRowID.ToString());
                //  brandsservice.DeleteById(deletedRowID);
            }

            return RedirectToAction("Index");
        }

        #endregion



        #region 新增

      


        [Authorize]
        public async Task<IActionResult> New()
        {

            //ViewBag.DataSource = _goodserver.GetPagesAsync(new PageParm()).Result.data.Items;
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New_btnSaveClose_Click(bannerViewModel bannerview, IFormFile filePhoto,
            string text)
        {
            var banner = new banner();

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
                     
                        string filePath = Path.Combine(fileDir, newFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            filePhoto.CopyTo(stream);
                        }

                        banner.Img = "/" + DateTime.Now.ToString("yyyyMM") + "/" + newFileName;
                    }
                }

                banner.CreateDate = DateTime.Now;
                banner.BannerOrder = bannerview.BannerOrder;
                banner.URL = bannerview.URL;
               

                var ret = await _bannerserver.AddAsync(banner);

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


    }
}
