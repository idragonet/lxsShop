using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
using FineUICore;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authorization;

namespace lxsShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SysConfigsController : lxsShop.Web.Controllers.BaseController
    {

        private readonly Isys_configsServer _sysconfigsserver;

        public SysConfigsController(Isys_configsServer sysconfigsserver)
        {
            _sysconfigsserver = sysconfigsserver;
        }


        #region 显示、删除


        [Authorize]
        public async Task<IActionResult> Index([FromQuery] PageParm request)
        {
            request.limit = 20;
            request.order = "ASC";
            request.field = "fieldCode";

            var post = await _sysconfigsserver.GetPagesAsync(request);
            return View(post.data.Items);

            // return View(post.data);
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
                await _sysconfigsserver.DeleteAsync(deletedRowID.ToString());
                //  brandsservice.DeleteById(deletedRowID);
            }

            return RedirectToAction("Index");
        }



        #endregion



        #region 新增




        [Authorize]
        public IActionResult New()
        {
            //ViewBag.DataSource = _goodserver.GetPagesAsync(new PageParm()).Result.data.Items;
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New_btnSaveClose_Click(sys_configs sysconfigs)
        {
           
            if (ModelState.IsValid)
            {
                var ret = await _sysconfigsserver.AddAsync(sysconfigs);

                if (ret.statusCode == 200)
                {
                    ActiveWindow.HidePostBack();
                }
                else
                {
                    return Ok(ret);
                }
            }

            return UIHelper.Result();
        }

        #endregion





        #region 编辑


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _sysconfigsserver.GetPagesAsync(new PageParm { id = id });
           
            return View(post.data.Items[0]);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_btnSaveClose_Click(sys_configs sysconfigs)
        {
           
            if (ModelState.IsValid)
            {
                var ret = await _sysconfigsserver.ModifyAsync(sysconfigs);

                if (ret.statusCode == 200)
                {
                    ActiveWindow.HidePostBack();
                }
                else
                {
                    return Ok(ret);
                }
            }

            return UIHelper.Result();
        }

        #endregion


    }
}
