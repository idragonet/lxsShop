using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.Common.ImgHelper;
using lxsShop.NewServices;
using Masuit.Tools.Logging;
using Microsoft.Extensions.Configuration;

namespace Horb_ShopSpider
{

    /// <summary>
    /// 获取爬虫结果插入商城
    /// </summary>
    public class Horb_ShopSpider
    {

        public static async Task<bool> run()
        {
            LogManager.Info("开始执行：ShopSpider.run" );

            var configure = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string readPicspath = configure["readPicspath"];
            if (string.IsNullOrEmpty(readPicspath)) new Exception("请在appsettings.json设置爬虫图片路径：readPicspath");

            string savePicspath = configure["savePicspath"];
            if (string.IsNullOrEmpty(readPicspath)) new Exception("请在appsettings.json设置保存图片路径：savePicspath");



            var db = new DbFactory().GetDb();
            var db2 = new DbFactory().GetDbMysql();

            try
            {
                List<horbShop> listMroShop = await db2.Queryable<horbShop>().ToListAsync();

                foreach (horbShop mroShop in listMroShop)
                {
                    if (!db.Queryable<goods>().Any(it => it.CopyURL == mroShop.Url))
                    {
                        //保存图片，如果没有图片就不插入商品（爬虫来保证一定要有图片才插入商品）

                        string picfile = readPicspath + "/" + mroShop.ID + ".jpg";
                        if (File.Exists(picfile))
                        {
                            /*var goodsCats = await db.Queryable<goods_cats>().Where(it => it.hcmroTypeID == mroShop.Class2).FirstAsync();
                            if(goodsCats==null) continue;*/

                            string goodsImg = UtilsShop.SavePic(readPicspath, savePicspath,mroShop.ID + ".jpg", AppContext.BaseDirectory + ("/uploads/"));
                            var newgood = new goods();
                            newgood.hcmroID = mroShop.ID;
                            newgood.CreateDate = DateTime.Now;
                            newgood.goodsSeoKeywords = mroShop.GoodsTitel;
                            newgood.goodsName = mroShop.GoodsTitel;
                            newgood.goodsDesc = mroShop.GoodsContent;
                            newgood.goodsSn = "MRO-" + mroShop.ID;
                            newgood.goodsImg = goodsImg;
                            newgood.goodsCatId = Convert.ToInt64(mroShop.Class2);
                            await db.Insertable(newgood).ExecuteCommandAsync();

                            LogManager.Info("成功插入商品，ID："+ mroShop.ID);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.Info("插入商品异常，ID：" + e.ToString());
                return false;
            }


            return true;

        }
       

    }
}
