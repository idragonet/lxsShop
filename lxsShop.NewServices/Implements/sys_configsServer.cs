using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.NewServices.Interfaces;

namespace lxsShop.NewServices.Implements
{
   public class sys_configsServer : BaseService<sys_configs>, Isys_configsServer
    {
        public async Task<string> GetKeyAsync(string parm)
        {
            try
            {
                var post = await Db.Queryable<sys_configs>().Where(g => g.fieldCode == parm)
                  //  .WithCache(60*30)
                    .FirstAsync();
                if (post!=null)  return post.fieldValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return "Null";
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(sys_configs parm)
        {
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                var dbres = await Db.Insertable(parm).ExecuteCommandAsync();
                if (dbres == 0)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                // Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = await Db.Deleteable<sys_configs>().Where(m => list.Contains(m.ID.ToString())).ExecuteCommandAsync();


            var res = new ApiResult<string>
            {
                statusCode = isok > 0 ? 200 : 500,
                data = isok > 0 ? "1" : "0",
                message = isok > 0 ? "删除成功~" : "删除失败~"
            };
            return res;
        }


        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<Page<sys_configs>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<sys_configs>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<sys_configs>()
                    .WhereIF(parm.id != 0, g => g.ID == parm.id)
                    .OrderByIF(!string.IsNullOrEmpty(parm.field), parm.field + " " + parm.order)
                    .ToPageAsync(parm.page, parm.limit);

                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(sys_configs parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };

            try
            {
                var dbres = await Db.Updateable(parm)
                    .Where(m => m.ID == parm.ID).ExecuteCommandAsync();
                if (dbres > 0)
                {
                    res.statusCode = (int)ApiEnum.Status;
                    res.message = "更新成功！";
                }
                else
                {
                    res.message = "更新失败！";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                // Logger<>.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }


    }
}
