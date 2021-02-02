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
  

    public class articlesServer : BaseService<articles>, IarticlesServer
    {


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(articles parm)
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
            var isok = await Db.Deleteable<articles>().Where(m => list.Contains(m.catId.ToString())).ExecuteCommandAsync();


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
        public async Task<ApiResult<Page<articlesViewModel>>> GetPagesAsync(PageParm parm)
        {
            /*res.data = await Db.Queryable<goods, goods_cats, brands>((g, gc, b) => new
                JoinQueryInfos(JoinType.Left, g.goodsCatId == gc.catId
                    , JoinType.Left, g.brandId == b.brandId))*/

            var res = new ApiResult<Page<articlesViewModel>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<articles,article_cats>((a,ac) => new
                        JoinQueryInfos(JoinType.Left, a.catId == ac.catId))
                    .WhereIF(!string.IsNullOrEmpty(parm.key), A => A.articleId == Convert.ToInt64(parm.key))
                    .Select((a, ac) => new articlesViewModel()
                    {
                        articleTitle = a.articleTitle,
                        articleContent = a.articleContent,
                        articleId = a.articleId,
                        catId = Convert.ToInt64(a.catId),
                        catName = ac.catName,
                        CreateDate = a.CreateDate,
                    })
                    .ToPageAsync(parm.page, parm.limit);

                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }



        public async Task<ApiResult<string>> ModifyAsync(articles parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };

            try
            {
                var dbres = await Db.Updateable<articles>().SetColumns(m => new articles()
                {
                    articleTitle = parm.articleTitle,
                    articleContent = parm.articleContent,
                    catId = parm.catId,
                    CreateDate = DateTime.Now
                }).Where(m => m.articleId == parm.articleId).ExecuteCommandAsync();
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
