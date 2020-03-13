﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entitys;
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


        private static List<goods_catsViewModel> resultNew = new List<goods_catsViewModel>();

        [Authorize]
        public IActionResult Category()
        {
            /*var postRepository = new CategoryRepository();
            var post = postRepository.FindById(1);*/

           
            LogManager.Info("记录一次消息");

              var post = goods_catsservice.FindAll();
             

             

            //   LogManager.Info("post:"+ post.Count());

          //  var result = _mapper.Map<List<goods_catsViewModel>>( goods_catsservice.FindAll());

            var result = post.MapTo<List<goods_catsViewModel>>();
            // var post2 = post as IEnumerable<goods_catsViewModel>;
            LogManager.Info("post2:" + result.Count());
            // Mapper.Map<FoodDto>(model);


            ResolveCollection(result, null, 0);

            return View(resultNew);
        }


        private static int ResolveCollection(List<goods_catsViewModel> result, goods_catsViewModel parentMenu, long level)
        {
            int count = 0;

            foreach (var goodsCatsViewModel in result.Where(m => m.parentId == level))
            {
                count++;
                resultNew.Add(goodsCatsViewModel);
                ResolveCollection(result, goodsCatsViewModel, goodsCatsViewModel.catId);
            }

            return count;
        }


        [Authorize]
        public IActionResult Brand()
        {
            return View();
        }


        public IActionResult Dept_DoPostBack()
        {
            throw new NotImplementedException();
        }
    }
}