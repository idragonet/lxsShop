using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ProductController : BaseController
    {

        //  private IMapper Mapper { get; }

        public Igoods_catsService goods_catsservice { get; }
       // public goodsRepository goods_service = new goodsRepository();

        //通过构造函数注入Service
        //  public ProductController(Mapper mapper, Igoods_catsService goods_catsservice)
        public ProductController(Igoods_catsService goods_catsservice)
        {
          //  _mapper = mapper;
            this.goods_catsservice = goods_catsservice;

            
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }



        #region 商品类别


        private static List<goods_catsViewModel> _resultNew;

        [Authorize]
        public IActionResult Category()
        {
            LogManager.Info("记录一次消息");

            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();
            ResolveCollection(result, null,0, 0);

            return View(_resultNew);
        }


        private static int ResolveCollection(List<goods_catsViewModel> result, goods_catsViewModel parentMenu,long parentId, int level)
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


        /*[Authorize]
        public IActionResult Brand()
        {
            return View();
        }*/




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dept_DoPostBack(string[] Grid1_fields, string actionType, int? deletedRowID)
        {
            if (actionType == "delete")
            {
                // 在操作之前进行权限检查
                /*if (!CheckPower("CoreDeptDelete"))
                {
                    CheckPowerFailWithAlert();
                    return UIHelper.Result();
                }*/


                var findByClause = goods_catsservice.FindByClause(m => m.parentId == deletedRowID.Value);
                if (findByClause != null)
                {
                    Alert.ShowInTop("删除失败！请先删除下级节点！");
                    return UIHelper.Result();
                }


                /*var goods = goods_service.FindByClause(m => m.goodsCatId == deletedRowID.Value);
                if (goods!=null)
                {
                    Alert.ShowInTop("删除失败！需要先清空该类别下带商品");
                    return UIHelper.Result();
                }*/


                if (goods_catsservice.DeleteById(deletedRowID))
                {
                  
                }

                /*int userCount = db.Users.Where(u => u.Dept.ID == deletedRowID.Value).Count();
                if (userCount > 0)
                {
                    Alert.ShowInTop("删除失败！需要先清空属于此部门的用户！");
                    return UIHelper.Result();
                }

                int childCount = db.Depts.Where(d => d.Parent.ID == deletedRowID.Value).Count();
                if (childCount > 0)
                {
                    Alert.ShowInTop("删除失败！请先删除子部门！");
                    return UIHelper.Result();
                }

                var dept = db.Depts.Where(d => d.ID == deletedRowID.Value).FirstOrDefault();
                db.Depts.Remove(dept);
                db.SaveChanges();*/
            }


            // DeptHelper.Reload();
            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();
            ResolveCollection(result, null, 0, 0);
            UIHelper.Grid("Grid1").DataSource(_resultNew, Grid1_fields);


            return UIHelper.Result();
        }

        

        #endregion

        #region 商品类别 新增


        [Authorize]
        public IActionResult CategoryNew()
        {
            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();

            var root =  new goods_catsViewModel();
            root.parentId = -1;
            root.catId = 0;
            root.catName = "--根节点--";
            result.Add(root);
         
            ResolveCollection(result, null, -1,0);


            ViewBag.DeptDataSource = _resultNew;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryNew_btnSaveClose_Click(
            [Bind(include:"catName,catSort,parentId")]
            goods_cats goods_cat) {

           
            if (ModelState.IsValid)
            {
                // 下拉列表的顶级节点值为-1
                if (goods_cat.parentId == -1)
                {
                    goods_cat.parentId = 0;
                }
               
                goods_cat.isShow = 1;
                goods_cat.CreateDate=DateTime.Now;
                goods_catsservice.Insert(goods_cat);

                //  db.Depts.Add(dept);
                //  db.SaveChanges();

                // 关闭本窗体（触发窗体的关闭事件）
                ActiveWindow.HidePostBack();
            }

            return UIHelper.Result();
        }



        #endregion


        #region 商品类别 编辑

        // GET: Admin/DeptEdit
        public IActionResult CategoryEdit(int id)
        {
            var current = goods_catsservice.FindById(id);

            /*Dept current = db.Depts
                .Where(m => m.ID == id).FirstOrDefault();*/
            if (current == null)
            {
                return Content("无效参数！");
            }

            var post = goods_catsservice.FindAll();
            var result = post.MapTo<List<goods_catsViewModel>>();
            _resultNew = new List<goods_catsViewModel>();

            var root = new goods_catsViewModel();
            root.parentId = -1;
            root.catId = 0;
            root.catName = "--根节点--";
            result.Add(root);

            ResolveCollection(result, null, -1, 0);

            ViewBag.DeptDataSource = _resultNew;


            return View(current.MapTo<goods_catsViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit_btnSaveClose_Click([Bind(include:"catId,catName,catSort,parentId,isShow")]
            goods_cats goods_cat)
        {
            if (ModelState.IsValid)
            {
                // 下拉列表的顶级节点值为-1
                // 下拉列表的顶级节点值为-1
                if (goods_cat.parentId == -1)
                {
                    goods_cat.parentId = 0;
                }
                goods_cat.CreateDate = DateTime.Now;

                goods_catsservice.Update(goods_cat);


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