﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;

namespace lxsShop.NewServices.Interfaces
{
    
    public interface IbrandsServer : IBaseService<brands>
    {


        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<brands>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(brands parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(brands parm);
    }
}