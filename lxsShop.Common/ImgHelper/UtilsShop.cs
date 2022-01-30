using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lxsShop.Common.ImgHelper
{
    public class UtilsShop
    {
        /// <summary>
        /// 保存标准规格商品图片
        /// </summary>
        /// <param name="filePhoto"></param>
        /// <param name="fileuploadsDir">网站上传本地路径</param>
        /// <returns></returns>
        public static string SavePic(IFormFile filePhoto, string fileuploadsDir)
        {
            string fileExt = Path.GetExtension(filePhoto.FileName);
            string fileDir = Path.Combine(fileuploadsDir, DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
            string newFileName300 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_300.jpg";
            string newFileName100 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt + "_100.jpg";
            string filePath = Path.Combine(fileDir, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                filePhoto.CopyTo(stream);
            }

            new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName300), 300, 300);
            new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName100), 100, 100);


            return "/" + DateTime.Now.ToString("yyyyMM") + "/" + newFileName;
        }



        /// <summary>
        /// 保存标准规格商品图片
        /// </summary>
        /// <param name="爬虫图片路径"></param>
        /// <param name="爬虫图片文件名"></param>
        /// <param name="保存图片目录"></param>
        /// <returns></returns>
        public static string SavePic(string SrcPath,string savePicspath, string SrcfileExt, string fileuploadsDir)
        {
            string fileDir = Path.Combine(savePicspath, DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + SrcfileExt;
            string newFileName300 = newFileName + "_300.jpg";
            string newFileName100 = newFileName + "_100.jpg";
            string filePath = Path.Combine(fileDir, newFileName);
           
            File.Copy(SrcPath+"/" + SrcfileExt, filePath, true);

            new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName300), 300, 300);
            new ThumbnailImage().MakeThumbnail(filePath, Path.Combine(fileDir, newFileName100), 100, 100);


            return "/" + DateTime.Now.ToString("yyyyMM") + "/" + newFileName;
        }
    }
}