using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Entitys;
using FineUICore;
using lxsShop.ViewModel;
using lxsShop.Web.Configs;
using lxsShop.Web.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lxsShop.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            }).Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            }).AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "text/html; charset=utf-8",
                    "application/xhtml+xml",
                    "application/atom+xml",
                    "image/svg+xml"
                });
            });
            services.AddControllers();


            // FineUI 和 MVC 服务
            services.AddFineUI(Configuration);
            services.AddMvc(options => {
                // 自定义模型绑定（Newtonsoft.Json）
                options.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
            });


            

            //添加 身份验证 服务
            /*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Admin/Index/Login");
                });*/


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //如果自己定义了Cookie名称，这里也需要指定Cookie名称 
                //.AddCookie("MyCookie", options => ...)
                .AddCookie(options =>
                {
                    //指定登陆页面的地址
                    options.LoginPath = "/Admin/Home/Login";
                    //指定无权访问的跳转页面地址
                    options.AccessDeniedPath = "/denied";
                });




            //添加对AutoMapper的支持
            services.AddAutoMapper(typeof(AutoMapperConfig));

            return RegisterAutofac(services);//注册Autofac


         
        }

        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //实例化Autofac容器
            var builder = new ContainerBuilder();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册
            builder.RegisterModule<AutofacModuleRegister>();
            //创建容器
            var Container = builder.Build();
            //第三方IOC接管 core内置DI容器
            return new AutofacServiceProvider(Container);
        }

     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages();//启用常见错误状态代码的默认纯文本处理程序

            //AutoMapperHelper
            app.UseStateAutoMapper();

            // 静态资源中间件
            app.UseStaticFiles();

            // FineUI 和 MVC 中间件（确保 UseFineUI 位于 UseMvc 的前面）
            app.UseFineUI();

            //使用身份验证服务  注意这句话要放在app.UseMvc的前面
            app.UseAuthentication();

            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                 /*routes.MapRoute(
                     name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");#1#
            });*/

            app.UseAuthentication();
            app.UseRouting();
          
          
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
              

                endpoints.MapControllerRoute(
                        name: "Areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    
                endpoints.MapControllerRoute("default", "{action=Index}/{id?}");


                //  endpoints.MapControllerRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
