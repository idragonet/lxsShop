using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.NewServices.Interfaces;
using Masuit.Tools.Security;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace lxsShop.NewServices.Implements
{


    public class AdminUsersServer : BaseService<AdminUsers>, IAdminUsersServer
    {
        #region  用户登录和授权菜单查询
        /// <summary>
        /// 用户登录实现
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<AdminUsers>> LoginAsync(AdminUsers parm,string IP)

        {
            var res = new ApiResult<AdminUsers>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var model = await Db.Queryable<AdminUsers>()
                        .Where(m => m.loginName == parm.loginName).FirstAsync();

                //一个IP 5分钟用户名密码错误10次，拒绝登录
                var dt=DateTime.Now.AddMinutes(-5);
                   var fail= log_loginsDb.Count(g => g.loginIp == IP &&  SqlFunc.DateIsSame(Convert.ToDateTime(g.loginTime),DateTime.Now,DateType.Hour));
                   if (fail >= 10)
                   {
                       res.message = "服务器忙~";
                       return res;
                   }



                if (model == null)
                {
                    log_loginsDb.Insert(new log_logins { LoginUser = parm.loginName, PASSWord = parm.loginPwd, loginIp = IP,loginTime = DateTime.Now });

                    res.message = "账号错误";
                    return res;
                }
             
                if (!model.loginPwd.Equals(parm.loginPwd.MDString()))// MD5加密
                {
                    log_loginsDb.Insert(new log_logins { LoginUser = parm.loginName, PASSWord = parm.loginPwd, loginIp = IP, loginTime = DateTime.Now });

                    res.message = "密码错误~";
                    return res;
                }
             
                //修改登录时间
                model.LastLoginTime = DateTime.Now;
                AdminUsersDb.Update(model);



                res.statusCode = (int)ApiEnum.Status;
                res.data = model;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                // Logger<>.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }
  
        #endregion

        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(AdminUsers parm)

        {
            throw new NotImplementedException();
        }

     

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = await Db.Deleteable<AdminUsers>().Where(m => list.Contains(m.ID.ToString())).ExecuteCommandAsync();
           
            var res = new ApiResult<string>
            {
                statusCode = isok > 0 ? 200 : 500,
                data = isok > 0 ? "1" : "0",
                message = isok > 0 ? "删除成功~" : "删除失败~"
            };
            return res;
        }

        public Task<ApiResult<string>> ModifyAsync(AdminUsers parm)
        {
            throw new NotImplementedException();
        }


      
       

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        async Task<ApiResult<Page<AdminUsers>>> IAdminUsersServer.GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<AdminUsers>>();
            try
            {
                var adminGuidList = new List<string>();
                //判断是否根据角色查询用户信息
                /*if (!string.IsNullOrEmpty(parm.guid))
                {
                    adminGuidList = await Db.Queryable<SysPermissions>()
                        .Where(m => m.RoleGuid == parm.guid && m.Types == 2)
                        .Select(m => m.AdminGuid).ToListAsync();
                }*/
                //查询角色
              
                res.data = await Db.Queryable<AdminUsers>()
                       // .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.DepartmentGuidList.Contains(parm.key))
                      //  .WhereIF(!string.IsNullOrEmpty(parm.guid), m => adminGuidList.Contains(m.Guid))
                        .OrderBy(m => m.CreateDate).ToPageAsync(parm.page, parm.limit);
                
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
               // Logger<>.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

      
    }
}
