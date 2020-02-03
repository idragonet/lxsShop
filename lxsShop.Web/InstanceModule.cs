using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace lxsShop.Web
{

    public class InstanceModule : Autofac.Module
    {
        //重写Autofac管道中的Load方法，在这里注入注册的内容
        protected override void Load(ContainerBuilder builder)
        {
            //以普通类的形式注册PlayPianoService
           // builder.RegisterType<PlayPianoService>();
        }

    }
}
