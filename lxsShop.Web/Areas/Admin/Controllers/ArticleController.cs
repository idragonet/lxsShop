using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineUICore;
using lxsShop.Repository;
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
        
        public articleRepository articleService = new articleRepository();

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
        public ActionResult Article_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
              
                var article = articleService.FindByClause(m => m.articleId == deletedRowID.Value);
                if (article != null)
                {
                    Alert.ShowInTop("删除失败！需要类别下面的全部文章.");
                    return UIHelper.Result();
                }


                if (article_catsservice.DeleteById(deletedRowID))
                {

                }


            }



            /*
            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();
            ResolveCollection(result, null, 0, 0);
            UIHelper.Grid("Grid1").DataSource(_resultNew, Grid1_fields);*/

            var post = article_catsservice.FindAll();
            var result = post.MapTo<List<article_catsViewModel>>();
            UIHelper.Grid("Grid1").DataSource(result, Grid1_fields);

            return UIHelper.Result();
        }



        #endregion



    }
}