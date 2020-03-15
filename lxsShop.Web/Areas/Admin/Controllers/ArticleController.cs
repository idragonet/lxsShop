using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineUICore;
using lxsShop.Services;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ArticleController : Controller
    {
        

        public Iarticle_catsService article_catsservice { get; }

        //通过构造函数注入Service
        public ArticleController(Iarticle_catsService article_catsservice)
        {
            this.article_catsservice = article_catsservice;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }



        #region 文章类别


        private static List<goods_catsViewModel> _resultNew;

        [Authorize]
        public IActionResult ArticleCats()
        {

            var post = article_catsservice.FindAll();
            var result = post.MapTo<List<article_catsViewModel>>();
            return View(result);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dept_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
             
                /*var findByClause = goods_catsservice.FindByClause(m => m.parentId == deletedRowID.Value);
              

                var goods = goods_service.FindByClause(m => m.goodsCatId == deletedRowID.Value);
                if (goods != null)
                {
                    Alert.ShowInTop("删除失败！需要先清空该类别下带商品");
                    return UIHelper.Result();
                }


                if (goods_catsservice.DeleteById(deletedRowID))
                {

                }*/


            }


           
            /*
            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();
            ResolveCollection(result, null, 0, 0);
            UIHelper.Grid("Grid1").DataSource(_resultNew, Grid1_fields);*/


            return UIHelper.Result();
        }



        #endregion



    }
}