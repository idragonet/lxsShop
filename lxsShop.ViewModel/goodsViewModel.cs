using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 商品 视图实体
    /// </summary>
    public class goodsViewModel
    {

        [Display(Name = "ID")]
        public long goodsId { get; set; }

        [Display(Name = "商品编号")]
        public string goodsSn { get; set; }


        [Display(Name = "商品名称")]
        [Required(ErrorMessage = "商品名称不要忘记填")]
        public string goodsName { get; set; }

        [Display(Name = "商品图片")]
        public string goodsImg { get; set; }

        [Display(Name = "推荐")]
        public bool isRecom { get; set; }


        [Display(Name = "新品")]
        public bool isNew { get; set; }


        [Display(Name = "品牌ID")]
        public long? brandId { get; set; }

        [Display(Name = "品牌")]
        public string brandName { get; set; }


        [Display(Name = "商品描述")]
        public string goodsDesc { get; set; }


        [Display(Name = "SEO")]
        public string goodsSeoKeywords { get; set; }


        [Display(Name = "商品类别ID")]
        public long goodsCatId { get; set; }

        [Display(Name = "商品类别")]
        public string catName { get; set; }

        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; }


        [Display(Name = "显示顺序(大排前)")]
        public long? ordering { get; set; }

    }
}
