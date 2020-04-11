using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lxsShop.ViewModel
{

    /// <summary>
    /// 商品类别 视图实体
    /// </summary>
    public class articleViewModel
    {

        [Display(Name = "ID")]
        public long articleId { get; set; }

        [Display(Name = "文章标题")]
        [Required(ErrorMessage = "文章标题不要忘记填")]
        public string articleTitle { get; set; }


        [Display(Name = "文章内容")]
        [Required(ErrorMessage = "文章内容不要忘记填")]
        public string articleContent { get; set; }


        [Display(Name = "文章类别")]
        [Required(ErrorMessage = "文章类别不要忘记填")]
        public long catId { get; set; }

        

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }



    }
}
