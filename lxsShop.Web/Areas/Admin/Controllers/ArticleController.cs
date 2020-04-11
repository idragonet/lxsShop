using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitys;
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



        // GET: Admin/DeptEdit
        public IActionResult ArticleCatsEdit(int id)
        {
            var current = article_catsservice.FindById(id);

            /*Dept current = db.Depts
                .Where(m => m.ID == id).FirstOrDefault();*/
            if (current == null)
            {
                return Content("无效参数！");
            }
            
            return View(current.MapTo<article_catsViewModel>());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleCatsEdit_btnSaveClose_Click([Bind(include:"catId,catName")]
            article_cats articlecats)
        {
            if (ModelState.IsValid)
            {
                // 下拉列表的顶级节点值为-1
                // 下拉列表的顶级节点值为-1

                articlecats.CreateDate = DateTime.Now;

                article_catsservice.Update(articlecats);


                // db.Entry(dept).State = EntityState.Modified;
                // db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }






        #region 商品类别 新增


        [Authorize]
        public IActionResult ArticleCatsNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleCatsNew_btnSaveClose_Click(
            [Bind(include:"catName")]
            article_cats articlecats)
        {


            if (ModelState.IsValid)
            {
                // 下拉列表的顶级节点值为-1
                articlecats.CreateDate = DateTime.Now;
                article_catsservice.Insert(articlecats);

                //  db.Depts.Add(dept);
                //  db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }



        #endregion



    }
}