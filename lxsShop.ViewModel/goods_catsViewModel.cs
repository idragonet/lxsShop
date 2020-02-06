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
      
        [Display(Name = "类别名称")]
        public string catName { get; set; }

        [Display(Name = "ParentId")]
        public long parentId { get; set; }



      
    }
}
