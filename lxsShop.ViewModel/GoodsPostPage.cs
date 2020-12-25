using System;
using System.Collections.Generic;
using System.Text;

namespace lxsShop.ViewModel
{
    public class GoodsPostPage
    {
        public string guid { get; set; }

        public int limit { get; set; } = 10;

        public int page { get; set; } = 1;
    }
}
