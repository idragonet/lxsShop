using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace lxsShop.Web
{
    public class AutofacComponent
    {
        /// <summary>
        /// 注册Autofac组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider Register(IServiceCollection services)
        {
            //实例化Autofac容器
            ContainerBuilder builder = new ContainerBuilder();
            //将collection中的服务填充到Autofac
            builder.Populate(services);
            //注册InstanceModule组件
            builder.RegisterModule<InstanceModule>();
            //创建容器
            IContainer container = builder.Build();
            //第三方容器接管Core内置的DI容器
            return new AutofacServiceProvider(container);
        }
    }
}
