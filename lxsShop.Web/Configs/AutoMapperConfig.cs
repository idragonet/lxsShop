using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entitys;
using lxsShop.ViewModel;
using lxsShop.Web.Extension;

namespace lxsShop.Web.Configs
{
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AutoMapperConfig()
        {
            // 配置 mapping 规则
            //
             CreateMap<goods_cats, goods_catsViewModel>();
             CreateMap<article_cats, article_catsViewModel>();
             CreateMap<articles, articlesViewModel>();

          //  AutoMapperHelper.UseStateAutoMapper(goods_cats, goods_catsViewModel);
        }
    }
}
