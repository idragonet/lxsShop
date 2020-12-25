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

namespace lxsShop.NewServices.Implements
{ 
    public class goodServer : BaseService<goods>, IgoodServer
    {
       

        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(goods parm)
        {
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                //判断是否存在
                
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
            var isok = await Db.Deleteable<goods>().Where(m => list.Contains(m.goodsId.ToString())).ExecuteCommandAsync();
           
           
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
        public async Task<ApiResult<Page<goodsViewModel>>> GetPagesAsync(PageParm param)
        {
            var res = new ApiResult<Page<goodsViewModel>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<goods, goods_cats, brands>((g, gc, b) => new
                        JoinQueryInfos(JoinType.Left, g.goodsCatId == gc.catId
                            , JoinType.Left, g.brandId== b.brandId))
                  //  .WhereIF(!string.IsNullOrEmpty(param.guid), (b, m, g) => b.QuestionGuid == param.guid)  //问题
                  //  .OrderByIF(param.attr == 1, (b, m, g) => b.AddTime, OrderByType.Desc)  //热门排序
                 //   .OrderBy((g, gc, b) => g.CreateDate, OrderByType.Desc)
                //  .OrderByIF(param.order.ToUpper() == "DESC", g => g.CreateDate, param.order.ToUpper())
                  .OrderByIF(!string.IsNullOrEmpty(param.field), param.field+" "+ param.order)
                    .Select((g, gc, b) => new goodsViewModel()
                    {
                        goodsId = g.goodsId,
                        goodsSn = g.goodsSn,
                        goodsName = g.goodsName,
                        goodsImg = g.goodsImg,
                        isRecom = g.isRecom,
                        isNew = g.isNew,
                        brandId = g.brandId,
                        brandName = b.brandName,
                        goodsDesc = g.goodsDesc,
                        goodsSeoKeywords = g.goodsSeoKeywords,
                        goodsCatId = g.goodsCatId,
                        catName = gc.catName,
                        CreateDate = g.CreateDate,
                    })
                    .ToPageAsync(param.page, param.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }



        public async Task<ApiResult<string>> ModifyAsync(goods parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };

            try
            {
               var dbres = await Db.Updateable<goods>().SetColumns(m => new goods()
                {
                    goodsName = parm.goodsName,
                    goodsDesc = parm.goodsDesc,
                    goodsSeoKeywords = parm.goodsSeoKeywords
                }).Where(m => m.goodsId == parm.goodsId).ExecuteCommandAsync();
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

        /*
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysAdmin parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = (int)ApiEnum.Error
            };
            try
            {
                //修改，判断用户是否和其它的重复
                var isExisteName = await Db.Queryable<SysAdmin>().AnyAsync(m => m.LoginName == parm.LoginName && m.Guid != parm.Guid);
                if (isExisteName)
                {
                    res.message = "用户名已存在，请更换~";
                    res.statusCode = (int)ApiEnum.ParameterError;
                    return await Task.Run(() => res);
                }

                parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                if (!string.IsNullOrEmpty(parm.DepartmentGuid))
                {
                    // 说明有父级  根据父级，查询对应的模型
                    var model = SysOrganizeDb.GetById(parm.DepartmentGuid);
                    parm.DepartmentGuidList = model.ParentGuidList;
                }
                //查询授权表，type=2 更新新的权限值
                //删除
                var authority = await Db.Deleteable<SysPermissions>().Where(m => m.AdminGuid == parm.Guid && m.Types == 2).ExecuteCommandAsync();
                //添加新的
                var authorityList = new List<SysPermissions>();
                foreach (var item in parm.RoleList)
                {
                    authorityList.Add(new SysPermissions()
                    {
                        RoleGuid = item.guid,
                        AdminGuid = parm.Guid,
                        Types = 2
                    });
                }
                await Db.Insertable(authorityList).ExecuteCommandAsync();

                var dbres = await Db.Updateable<SysAdmin>().SetColumns(m => new SysAdmin()
                {
                    LoginName = parm.LoginName,
                    LoginPwd = parm.LoginPwd,
                    RoleGuid = parm.RoleGuid,
                    DepartmentName = parm.DepartmentName,
                    DepartmentGuid = parm.DepartmentGuid,
                    DepartmentGuidList = parm.DepartmentGuidList,
                    TrueName = parm.TrueName,
                    Number = parm.Number,
                    Sex = parm.Sex,
                    Mobile = parm.Mobile,
                    Email = parm.Email,
                    Status = parm.Status
                }).Where(m => m.Guid == parm.Guid).ExecuteCommandAsync();
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
                Logger<>.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }*/
    }
}
