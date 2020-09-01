using Autofac;
using Autofac.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Models;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.ComponentManagement.Interceptors;
using Castle.Components.DictionaryAdapter.Xml;
using Autofac.Core.Resolving;
using WpfSamples.Models.ComponentManagement;
using WpfSamples.Infrastructure.ComponentManagement;
using ModelSamle.Services;
using Autofac.Extras.DynamicProxy;

namespace ModelSamle
{
    public class Startup
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<InfrastructureDependencyModule>();
            builder.RegisterModule<ModelsDependencyModule>();

            builder.RegisterType<ProductCreateService>()
                .AsSelf()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(TraceInterceptor), typeof(TransactionInterceptor))
                ;
            return builder.Build();
        }
    }
}
