using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using FineUICore;
using lxsShop.Repository;
using lxsShop.Services;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : lxsShop.Web.Controllers.BaseController
    {
        public IbrandsService brandsCatsservice { get; }

        //通过构造函数注入Service
        public BrandsController(IbrandsService brands_catsservice)
        {
            brandsCatsservice = brands_catsservice;
        }


        #region 显示、删除

        [Authorize]
        public IActionResult Brands()
        {
            var post = brandsCatsservice.FindAll();
            var result = post.MapTo<List<brandsViewModel>>();


            return View(result);
        }


        public brandsRepository BrandsRepository = new brandsRepository();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brands_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete") BrandsRepository.DeleteById(deletedRowID);

            var post = BrandsRepository.FindAll();
            var result = post.MapTo<List<brandsViewModel>>();
            UIHelper.Grid("Grid1").DataSource(result, Grid1_fields);

            return UIHelper.Result();
        }

        #endregion


        #region 文章编辑

        // GET: Admin/DeptEdit
        public IActionResult BrandsEdit(int id)
        {
            var current = BrandsRepository.FindById(id);

            /*Dept current = db.Depts
                .Where(m => m.ID == id).FirstOrDefault();*/
            if (current == null) return Content("无效参数！");

            ViewBag.BrandsDataSource = BrandsRepository.FindAll();


            return View(current.MapTo<brandsViewModel>());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandsEdit_btnSaveClose_Click([Bind(include: "brandId,brandName,brandImg")]
            brands brandsEdit, IFormFile filePhoto)
        {
            if (ModelState.IsValid)
            {
                if (filePhoto != null)
                {
                    var fileName = filePhoto.FileName;

                    if (!ValidateFileType(fileName))
                    {
                        // 清空文件上传组件
                        UIHelper.FileUpload("filePhoto").Reset();

                        ShowNotify("无效的文件类型！");
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

                        /*
                         UIHelper.Label("labResult").Text("<p>文件路径：" + filePhoto.FileName + "</p>" +
                                                         "<p>用户名：" + values["tbxUserName"] + "</p>" +
                                                         "<p>头像：<br /><img src=\"" + Url.Content("~/uploads/Logo" + fileName) + "\" /></p>",
                            encodeText: false);

                        // 清空表单字段
                        UIHelper.SimpleForm("SimpleForm1").Reset();
                        */
                    }
                }


                // 下拉列表的顶级节点值为-1

                brandsEdit.CreateDate = DateTime.Now;


                BrandsRepository.Update(brandsEdit);


                // db.Entry(dept).State = EntityState.Modified;
                // db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }

        #endregion
    }
}