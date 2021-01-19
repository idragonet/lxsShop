using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;

namespace lxsShop.NewServices.Interfaces
{
 

    /// <summary>
    /// 管理员接口
    /// </summary>
    public interface IAdminUsersServer : IBaseService<AdminUsers>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<AdminUsers>> LoginAsync(AdminUsers parm, string IP);


        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<AdminUsers>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(AdminUsers parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(AdminUsers parm, string NewPwd);
    }
}
