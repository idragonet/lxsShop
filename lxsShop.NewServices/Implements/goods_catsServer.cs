using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.NewServices.IBaseServices;
using lxsShop.NewServices.Interfaces;

namespace lxsShop.NewServices.Implements
{

   public class goods_catsServer : BaseService<goods_cats>, Igoods_catsServer
    {


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(goods_cats parm)
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
            var isok = await Db.Deleteable<goods_cats>().Where(m => list.Contains(m.catId.ToString())).ExecuteCommandAsync();


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
        public async Task<ApiResult<Page<goods_cats>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<goods_cats>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                if (!string.IsNullOrEmpty(parm.where) && parm.where == "getgoodscat") //通过类别集合ID查询类别
                {
                    res.data = await Db.Queryable<goods_cats>()
                        .In(it => it.catId, parm.IdList)
                        .ToPageAsync(parm.page, parm.limit); ;
                }
                else if(!string.IsNullOrEmpty(parm.where) && parm.where == "catid") //通过商品归属二级类别ID获取到一级类别名称
                {
                    res.data = await Db.Queryable<goods_cats>()
                        .Where(x=>x.catId==parm.attr)
                        .ToPageAsync(parm.page, parm.limit); ;
                }
                else if (!string.IsNullOrEmpty(parm.where) && parm.where == "class3") //通过二姐类别ID或者三级类别ID查询同级全部三级类别
                {
                    //加载逻辑：当前ID是三级分类或者当前ID是二级分类而且下带3级分类就加载内容；当前是1级分类、当前ID是二级分类而且没下带3级分类就不加载。


                    //判断当前类别ID是否二级：对应parentId不是0+当前ID作为parentId能查询到有下级
                    //parm.attr是当前料品ID
                    var isparentIdnot0 = await Db.Queryable<goods_cats>()
                        .AnyAsync(x => x.catId == parm.attr && x.parentId > 0);
                    var ishaveclass3 = await Db.Queryable<goods_cats>()
                        .AnyAsync(x => x.parentId == parm.attr);
                    if (isparentIdnot0 && ishaveclass3)
                    {
                        res.data = await Db.Queryable<goods_cats>()
                            .Where(x => x.parentId == parm.attr)
                            .ToPageAsync(parm.page, 300); ;
                    }
                    else if(isparentIdnot0)  //判断当前料品ID是否三级：不是二级+不是一级就是三级
                    {
                        var cats_parentId = await Db.Queryable<goods_cats>()
                            .FirstAsync(x => x.catId == parm.attr);
                       if (cats_parentId!=null)
                       {
                           parm.attr = Convert.ToInt32(cats_parentId.parentId);
                           res.data = await Db.Queryable<goods_cats>()
                               .Where(x => x.parentId == parm.attr)
                               .ToPageAsync(parm.page, 300); ;
                       }
                    }

                    //如果 当前ID是二级分类而且没下带3级分类就不加载
                    if (res.data != null && res.data.Items != null && res.data.Items.Count>0)
                    {
                        var parentId = res.data.Items[0].parentId;
                        if (await Db.Queryable<goods_cats>()
                                .AnyAsync(x => x.catId == parentId && x.parentId == 0))
                        {
                            res.data.Items = null;
                        }
                    }
                  

                }
                else
                {
                    res.data = await Db.Queryable<goods_cats>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.catId == Convert.ToInt64(parm.key))
                        .WhereIF(!string.IsNullOrEmpty(parm.where) && parm.where == "parentId", m => m.parentId == Convert.ToInt64(parm.attr))
                        //  .OrderBy(m => m.Sort)
                        .ToPageAsync(parm.page, parm.limit);
                }

              

                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }



        public async Task<ApiResult<string>> ModifyAsync(goods_cats parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };

            try
            {
                var dbres = await Db.Updateable<goods_cats>().SetColumns(m => new goods_cats()
                {
                    
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
