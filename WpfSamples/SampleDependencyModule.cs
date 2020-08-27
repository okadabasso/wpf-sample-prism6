using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WpfSamples.ComponentManagement;
using WpfSamples.Services;
using Autofac.Core;
using Castle.Components.DictionaryAdapter.Xml;

namespace WpfSamples
{
    public class SampleDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ILogger>((c) => {
                return LogManager.GetLogger(c.GetComponentType().FullName);
            });
            
            builder.RegisterType<TraceInterceptor>();
            builder.RegisterType<FooService>().AsSelf()
                 .EnableClassInterceptors()
                 .InterceptedBy(typeof(TraceInterceptor));

            builder.RegisterType<ViewModels.SampleViewModel>().AsSelf()
             .EnableClassInterceptors()
             .InterceptedBy(typeof(TraceInterceptor));

        }
    }
}
