using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace lxsShop.Common.ImgHelper
{
  public  class ThumbnailImage
    {
        ///<summary>
        /// 生成缩略图 （等比例，水平、垂直方向居中，画布白色背景填充）
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径，含文件名）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径，含文件名）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        public void  MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0; //缩略图在画布上的X放向起始点
            int y = 0; //缩略图在画布上的Y放向起始点
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            int dw = 0;
            int dh = 0;

            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
            {
                //宽比高大，以宽为准
                dw = originalImage.Width * towidth / originalImage.Width;
                dh = originalImage.Height * toheight / originalImage.Width;
                x = 0;
                y = (toheight - dh) / 2;
            }
            else
            {
                //高比宽大，以高为准
                dw = originalImage.Width * towidth / originalImage.Height;
                dh = originalImage.Height * toheight / originalImage.Height;
                x = (towidth - dw) / 2;
                y = 0;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(x, y, dw, dh),
             new Rectangle(0, 0, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //以Jpeg格式保存缩略图(KB最小)
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
              //  outthumbnailPath = thumbnailPath;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

    }
}
