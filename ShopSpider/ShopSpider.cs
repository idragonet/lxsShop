﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Entitys;
using lxsShop.Common.ImgHelper;
using lxsShop.NewServices;
using Microsoft.Extensions.Configuration;

namespace ShopSpider
{

    /// <summary>
    /// 获取爬虫结果插入商城
    /// </summary>
    public class ShopSpider
    {

        public static async Task<bool> run()
        {
            var configure = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string readPicspath = configure["readPicspath"];
            if (string.IsNullOrEmpty(readPicspath)) new Exception("请在appsettings.json设置爬虫图片路径：readPicspath");

            string savePicspath = configure["savePicspath"];
            if (string.IsNullOrEmpty(readPicspath)) new Exception("请在appsettings.json设置保存图片路径：savePicspath");



            var db = new DbFactory().GetDb();
            var db2 = new DbFactory().GetDbMysql();

            try
            {
                List<MROShop> listMroShop = await db2.Queryable<MROShop>().ToListAsync();

                foreach (MROShop mroShop in listMroShop)
                {
                    if (!db.Queryable<goods>().Any(it => it.hcmroID == mroShop.ID))
                    {
                        //保存图片，如果没有图片就不插入商品（爬虫来保证一定要有图片才插入商品）

                        string picfile = readPicspath + "/" + mroShop.ID + ".jpg";
                        if (File.Exists(picfile))
                        {
                            var goodsCats = await db.Queryable<goods_cats>().Where(it => it.hcmroTypeID == mroShop.Class2).FirstAsync();
                            if(goodsCats==null) continue;

                            string goodsImg = UtilsShop.SavePic(readPicspath, savePicspath,mroShop.ID + ".jpg", AppContext.BaseDirectory + ("/uploads/"));
                            var newgood = new goods();
                            newgood.hcmroID = mroShop.ID;
                            newgood.CreateDate = DateTime.Now;
                            newgood.goodsSeoKeywords = mroShop.GoodsTitel;
                            newgood.goodsName = mroShop.GoodsTitel;
                            newgood.goodsDesc = mroShop.GoodsContent;
                            newgood.goodsSn = "MRO-" + mroShop.ID;
                            newgood.goodsImg = goodsImg;
                            newgood.goodsCatId = goodsCats.catId;
                            await db.Insertable(newgood).ExecuteCommandAsync();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


            return true;

        }
       

    }
}
