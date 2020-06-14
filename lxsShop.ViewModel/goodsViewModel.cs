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
        public long goodsImg { get; set; }

        [Display(Name = "推荐")]
        public int isRecom { get; set; }


        [Display(Name = "新品")]
        public int isNew { get; set; }


        [Display(Name = "品牌ID")]
        public long brandId { get; set; }




        [Display(Name = "商品描述")]
        [Required(ErrorMessage = "商品描述不要忘记填")]
        public string goodsDesc { get; set; }


        [Display(Name = "关键词")]
        public string goodsSeoKeywords { get; set; }


        [Display(Name = "商品类别")]
        public long goodsCatId { get; set; }



        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }
        
    }
}
