using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 品牌 视图实体
    /// </summary>
    public class brandsViewModel
    {

        [Display(Name = "ID")]
        public long brandId { get; set; }

        [Display(Name = "品牌名称")]
        [Required(ErrorMessage = "品牌名称不要忘记填")]
        public string brandName { get; set; }


        [Display(Name = "品牌LOGO")]
        [Required(ErrorMessage = "品牌LOGO不要忘记填")]
        public string brandImg { get; set; }


    }
}
