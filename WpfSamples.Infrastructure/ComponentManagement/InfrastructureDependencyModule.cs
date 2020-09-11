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
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace WpfSamples.Infrastructure.ComponentManagement
{
    public class InfrastructureDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>();

            builder.Register<Func<Microsoft.Extensions.Logging.ILogger>>(c => {
                var factory = c.Resolve<ILoggerFactory>();
                return () => { return factory.CreateLogger("default"); };
            }).PropertiesAutowired();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            builder.RegisterType<TraceInterceptorAsync>().AsSelf();
            builder.RegisterType<TransactionInterceptorAsync>().As<TransactionInterceptorAsync>();
            builder.RegisterType<TraceInterceptor>().AsSelf();
            builder.RegisterType<TransactionInterceptor>().As<TransactionInterceptor>();


        }
    }
}
