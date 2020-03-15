using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 商品类别 视图实体
    /// </summary>
    public class article_catsViewModel
    {

        [Display(Name = "ID")]
        public long catId { get; set; }

        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "类别名称不要忘记填")]
        public string catName { get; set; }



        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }



    }
}
