using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.NewServices.Interfaces;
using lxsShop.ViewModel;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SqlSugar.Extensions;

namespace lxsShop.NewServices.Implements
{
    public class goodServer : BaseService<goods>, IgoodServer
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(goods parm)
        {
            var res = new ApiResult<string>() {statusCode = 200};
            try
            {
                //判断商品编号是否存在,如果不存在自动产生
                if (string.IsNullOrEmpty(parm.goodsSn))
                {
                    int maxId1 = goodsDb.AsQueryable().Max(it => it.goodsId).ObjToInt() + 1; //拉姆达
                    parm.goodsSn = "BONKE-" + maxId1;
                }


                if (!string.IsNullOrEmpty(parm.goodsSn))
                {
                    parm.goodsSn = parm.goodsSn.Trim().ToUpper();
                    //判断是否存在
                    var isExt = goodsDb.IsAny(m => m.goodsSn == parm.goodsSn);
                    if (isExt)
                    {
                        res.statusCode = (int) ApiEnum.ParameterError;
                        res.message = "商品编号已存在~";
                    }
                    else
                    {
                        var dbres = await Db.Insertable(parm).ExecuteCommandAsync();
                        if (dbres == 0)
                        {
                            res.statusCode = (int)ApiEnum.Error;
                            res.message = "插入数据失败~";
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                res.statusCode = (int) ApiEnum.Error;
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
            var isok = await Db.Deleteable<goods>().Where(m => list.Contains(m.goodsId.ToString()))
                .ExecuteCommandAsync();


            var res = new ApiResult<string>
            {
                statusCode = isok > 0 ? 200 : 500,
                data = isok > 0 ? "1" : "0",
                message = isok > 0 ? "删除成功~" : "删除失败~"
            };
            return res;
        }


        /// <summary>
        /// 获得产品列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<Page<goodsViewModel>>> GetPagesAsync(PageParm param)
        {
            var res = new ApiResult<Page<goodsViewModel>>() {statusCode = (int) ApiEnum.Error};
            try
            {

                /* res.data = await Db.Queryable<goods_cats>()
                    .In(it => it.catId, parm.IdList)
                    .ToPageAsync(parm.page, parm.limit);  */

                if (param.IdList!=null && param.IdList.Count > 0)
                {
                    res.data = await Db.Queryable<goods, goods_cats, brands>((g, gc, b) => new
                       JoinQueryInfos(JoinType.Left, g.goodsCatId == gc.catId
                           , JoinType.Left, g.brandId == b.brandId))
                        .In(g => g.goodsCatId, param.IdList)
                        .OrderBy(g=>g.CreateDate,OrderByType.Desc)
                   .Select((g, gc, b) => new goodsViewModel()
                   {
                       goodsId = g.goodsId,
                       goodsSn = g.goodsSn,
                       goodsName = g.goodsName,
                       goodsImg = g.goodsImg,
                       isRecom = Convert.ToBoolean(g.isRecom),
                       isNew = Convert.ToBoolean(g.isNew),
                       brandId = g.brandId,
                       brandName = b.brandName,
                       goodsDesc = g.goodsDesc,
                       goodsSeoKeywords = g.goodsSeoKeywords,
                       goodsCatId = g.goodsCatId,
                       catName = gc.catName,
                       CreateDate = g.CreateDate,
                       ordering = g.ordering
                   })
                   .ToPageAsync(param.page, param.limit);
                }
                else
                {

                    res.data = await Db.Queryable<goods, goods_cats, brands>((g, gc, b) => new
                            JoinQueryInfos(JoinType.Left, g.goodsCatId == gc.catId
                                , JoinType.Left, g.brandId == b.brandId))
                        .WhereIF(param.id != 0, g => g.goodsId == param.id)
                        .WhereIF(!string.IsNullOrEmpty(param.where) && param.where == "parentId", (g, gc, b) => gc.parentId == param.attr) //归属上级类别
                        .WhereIF(!string.IsNullOrEmpty(param.where) && param.where == "goodsCatId", (g, gc, b) => g.goodsCatId == param.attr) //查询类别下的商品

                        .WhereIF(!string.IsNullOrEmpty(param.where) && param.where == "search" && param.attr > 0, (g, gc, b) => g.goodsName.Contains(param.key) && g.goodsCatId == param.attr) //搜索 + 指定类别
                        .WhereIF(!string.IsNullOrEmpty(param.where) && param.where == "search", (g, gc, b) => g.goodsName.Contains(param.key) || g.goodsSn.Contains(param.key)) //搜索
                        .WhereIF(!string.IsNullOrEmpty(param.where) && param.where == "search2", (g, gc, b) => gc.catName==param.key) //搜索类别名称

                        //  .WhereIF(!string.IsNullOrEmpty(param.guid), (b, m, g) => b.QuestionGuid == param.guid)  //问题
                        //  .OrderByIF(param.attr == 1, (b, m, g) => b.AddTime, OrderByType.Desc)  //热门排序
                        //   .OrderBy((g, gc, b) => g.CreateDate, OrderByType.Desc)
                        //  .OrderByIF(param.order.ToUpper() == "DESC", g => g.CreateDate, param.order.ToUpper())
                        .OrderByIF(!string.IsNullOrEmpty(param.field), param.field)
                        .Select((g, gc, b) => new goodsViewModel()
                        {
                            goodsId = g.goodsId,
                            goodsSn = g.goodsSn,
                            goodsName = g.goodsName,
                            goodsImg = g.goodsImg,
                            isRecom = Convert.ToBoolean(g.isRecom),
                            isNew = Convert.ToBoolean(g.isNew),
                            brandId = g.brandId,
                            brandName = b.brandName,
                            goodsDesc = g.goodsDesc,
                            goodsSeoKeywords = g.goodsSeoKeywords,
                            goodsCatId = g.goodsCatId,
                            catName = gc.catName,
                            CreateDate = g.CreateDate,
                            ordering = g.ordering
                        })
                        .ToPageAsync(param.page, param.limit);
                }

                res.statusCode = (int) ApiEnum.Status;
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
        public async Task<ApiResult<string>> ModifyAsync(goods parm)
        {


            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                        var dbres = await Db.Updateable(parm)
                            .Where(m => m.goodsId == parm.goodsId)
                            .ExecuteCommandAsync();

                     

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
        /// 修改商品类别
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyCatAsync(long catid,List<long> googList)
        {
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
              var updatecount=  Db.Updateable<goods>()
                    .SetColumns(x => x.goodsCatId == catid)
                    .Where(x=> googList.Contains(x.goodsId))
                    .ExecuteCommand();


                if (updatecount == 0)
                {
                    res.success = false;
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "更新数据失败~";
                }
                else
                {
                    res.success= true;
                    res.message= "更新成功"+updatecount+"条数据.";
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



    }
}