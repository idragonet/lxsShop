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
        

        public Iarticle_catsService articleCatsservice { get; }
       // public IarticleService articleservice { get; }
        
        public articleRepository ArticleRepository = new articleRepository();

        //通过构造函数注入Service
        public ArticleController(Iarticle_catsService article_catsservice)
        {
            this.articleCatsservice = article_catsservice;
        }


        #region 文章显示、删除


        [Authorize]
        public IActionResult Article()
        {
            var post = ArticleRepository.FindAll();
            var result = post.MapTo<List<articlesViewModel>>();

            var post2 = ArticleRepository.FindAll2();
           // var result2= post2.MapTo<List<articlesViewModel>>();



            return View(post2);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Article_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
                ArticleRepository.DeleteById(deletedRowID);
            }

            var post = ArticleRepository.FindAll();
            var result = post.MapTo<List<articlesViewModel>>();
            UIHelper.Grid("Grid1").DataSource(result, Grid1_fields);

            return UIHelper.Result();
        }


        #endregion


        #region 文章编辑

        // GET: Admin/DeptEdit
        public IActionResult ArticleEdit(int id)
        {
            var current = ArticleRepository.FindById(id);

            /*Dept current = db.Depts
                .Where(m => m.ID == id).FirstOrDefault();*/
            if (current == null)
            {
                return Content("无效参数！");
            }

            ViewBag.ArticleCatsDataSource = articleCatsservice.FindAll();


            return View(current.MapTo<articlesViewModel>());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticlesEdit_btnSaveClose_Click([Bind(include:"articleId,articleTitle,catId")]
            articles articlesEdit, string text)
        {
            if (ModelState.IsValid)
            {
                // 下拉列表的顶级节点值为-1
                // 下拉列表的顶级节点值为-1

                articlesEdit.CreateDate = DateTime.Now;
                articlesEdit.articleContent = text;

                ArticleRepository.Update(articlesEdit);


                // db.Entry(dept).State = EntityState.Modified;
                // db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }




        #endregion



        #region 文章 新增


        [Authorize]
        public IActionResult ArticleNew()
        {
            ViewBag.ArticleCatsDataSource = articleCatsservice.FindAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleNew_btnSaveClose_Click(
            [Bind(include:"articleTitle,catId")]
            articles articlesEdit, string text)
        {
            if (ModelState.IsValid)
            {
                articlesEdit.CreateDate = DateTime.Now;
                articlesEdit.articleContent = text;

                ArticleRepository.Insert(articlesEdit);

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }



        #endregion




        #region 文章类别 显示、删除



        [Authorize]
        public IActionResult ArticleCats()
        {
            var post = articleCatsservice.FindAll();
            var result = post.MapTo<List<article_catsViewModel>>();
            return View(result);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleCats_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
              
                var article = ArticleRepository.FindByClause(m => m.articleId == deletedRowID.Value);
                if (article != null)
                {
                    Alert.ShowInTop("删除失败！需要类别下面的全部文章.");
                    return UIHelper.Result();
                }


                if (articleCatsservice.DeleteById(deletedRowID))
                {

                }
            }



            /*
            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();
            ResolveCollection(result, null, 0, 0);
            UIHelper.Grid("Grid1").DataSource(_resultNew, Grid1_fields);*/

            var post = articleCatsservice.FindAll();
            var result = post.MapTo<List<article_catsViewModel>>();
            UIHelper.Grid("Grid1").DataSource(result, Grid1_fields);

            return UIHelper.Result();
        }


        #endregion


        #region 文章类别编辑

        // GET: Admin/DeptEdit
        public IActionResult ArticleCatsEdit(int id)
        {
            var current = articleCatsservice.FindById(id);

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

                articleCatsservice.Update(articlecats);


                // db.Entry(dept).State = EntityState.Modified;
                // db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }


        

        #endregion


        #region 文章类别 新增


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
                articlecats.CreateDate = DateTime.Now;
                articleCatsservice.Insert(articlecats);

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }



        #endregion



    }
}