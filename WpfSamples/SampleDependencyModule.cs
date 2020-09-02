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
using WpfSamples.Services;
using Autofac.Core;
using Castle.Components.DictionaryAdapter.Xml;
using Autofac.Core.Resolving;

namespace WpfSamples
{
    public class SampleDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // transactional
            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(t => t.GetCustomAttribute<DependencyObjectAttribute>()?.Transactional ?? false )
                 .AsSelf()
                 .EnableClassInterceptors()
                 .InterceptedBy(typeof(TraceInterceptor), typeof(TransactionInterceptor) )
                 .InstancePerLifetimeScope()
                 ;
            // trace
            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(t => (!t.GetCustomAttribute<DependencyObjectAttribute>()?.Transactional) ?? false)
                 .AsSelf()
                 .AsImplementedInterfaces()
                 .EnableClassInterceptors()
                 .InterceptedBy(typeof(TraceInterceptor))
                 ;
        }
    }
}
