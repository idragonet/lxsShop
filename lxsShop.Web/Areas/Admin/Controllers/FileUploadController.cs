using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FineUICore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace lxsShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class FileUploadController : Controller
    {
        private IHostingEnvironment hostingEnv;

        public FileUploadController(IHostingEnvironment env)
        {
            hostingEnv = env;
        }

        public async Task UeditorUpload([FromForm] IFormCollection formData)
        {
            

            // var files = Request.Form.Files;
            var files = formData.Files;//等价于Request.Form.Files
            long size = files.Sum(f => f.Length);


            string callback = Request.Query["callback"];
            string editorId = Request.Query["editorid"];
            if (files != null && size > 0)
            {
                var file = files[0];
                string fileDir = Path.Combine(PageContext.MapWebPath("~/uploads/" ), DateTime.Now.ToString("yyyyMM"));
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }

                string fileExt = Path.GetExtension(file.FileName);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                string filePath = Path.Combine(fileDir, newFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }


                string URL = "/uploads/" + DateTime.Now.ToString("yyyyMM") +"/" + newFileName;
                var fileInfo = getUploadInfo(URL, file.FileName,
                    Path.GetFileName(filePath), file.Length, fileExt);
                string json = BuildJson(fileInfo);

                Response.ContentType = "text/plain; charset=utf-8";
                if (callback != null)
                {
                    await Response.WriteAsync(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback,
                        json));
                }
                else
                {
                    await Response.WriteAsync(json);
                }

            }

        }

         private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] {"originalName", "name", "url", "size", "state", "type"};
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == "size")
                {
                    fields.Add(String.Format("\"{0}\": {1}", keys[i], info[keys[i]]));
                }
                else
                {
                    fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
                }
            }

            return "{" + String.Join(",", fields) + "}";
        } 
      

        /**
       * 获取上传信息
       * @return Hashtable
       */
        private Hashtable getUploadInfo(string URL, string originalName, string name, long size, string type,
            string state = "SUCCESS")
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);
            infoList.Add("originalName", originalName);
            infoList.Add("name", Path.GetFileName(URL));
            infoList.Add("size", size);
            infoList.Add("type", Path.GetExtension(originalName));

            return infoList;
        }
    }
}