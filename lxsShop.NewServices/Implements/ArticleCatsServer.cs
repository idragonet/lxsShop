using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using SqlSugar;

namespace lxsShop.NewServices.Implements
{
   public class ArticleCatsServer : BaseService<article_cats>, IArticleCatsServer
   {


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(article_cats parm)
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
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = await Db.Deleteable<article_cats>().Where(m => list.Contains(m.catId.ToString())).ExecuteCommandAsync();


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
        public async Task<ApiResult<Page<article_cats>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<article_cats>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<article_cats>()
                    .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.catId==Convert.ToInt64(parm.key))
                    //  .OrderBy(m => m.Sort)
                    .ToPageAsync(parm.page, parm.limit);

                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }



        public async Task<ApiResult<string>> ModifyAsync(article_cats parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };

            try
            {
                var dbres = await Db.Updateable<article_cats>().SetColumns(m => new article_cats()
                {
                    catName =  parm.catName,
                     CreateDate = DateTime.Now
                }).Where(m => m.catId == parm.catId).ExecuteCommandAsync();
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
