using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.ComponentManagement.Interceptors;
using Autofac.Core;
using Castle.Components.DictionaryAdapter.Xml;
namespace WpfSamples.Infrastructure.ComponentManagement
{
    public class InfrastructureDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ILogger>((c) => {
                return LogManager.GetLogger(c.GetComponentType().FullName);
            });

            builder.RegisterType<TraceInterceptor>();
            builder.RegisterType<TransactionInterceptor>();


        }
    }
}
