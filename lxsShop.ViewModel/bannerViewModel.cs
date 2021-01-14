using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 品牌 视图实体
    /// </summary>
    public class bannerViewModel
    {

        [Display(Name = "ID")]
        public long ID { get; set; }

        [Display(Name = "滚动图片")]
        public string Img { get; set; }


        [Display(Name = "URL")]
        [Required(ErrorMessage = "URL不要忘记填")]
        public string URL { get; set; }



        [Display(Name = "显示排序(大优先)")]
        [Required(ErrorMessage = "显示排序不要忘记填")]
        public long BannerOrder { get; set; }
        

    }
}
