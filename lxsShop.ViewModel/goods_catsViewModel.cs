using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 商品类别 视图实体
    /// </summary>
    public class goods_catsViewModel
    {

        [Display(Name = "ID")]
        public long catId { get; set; }

        [Display(Name = "类别名称")]
        public string catName { get; set; }

        [Display(Name = "ParentId")]
        public long parentId { get; set; }

        [Display(Name = "排序")]
        public long catSort { get; set; }


        [Display(Name = "显示")]
        public long isShow { get; set; }
        

    }
}
