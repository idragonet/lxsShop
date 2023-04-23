using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.ViewModel;

namespace lxsShop.NewServices.Interfaces
{
  

    public interface IgoodServer : IBaseService<goods>
    {
 

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<goodsViewModel>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(goods parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(goods parm);


        /// <summary>
        /// 批量修改商品归属类别
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="googList"></param>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyCatAsync(long catid, List<long> googList);
    }
}
