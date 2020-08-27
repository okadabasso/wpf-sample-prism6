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

            builder.Register<ILogger>((c) => {
                if(c is IInstanceLookup)
                {
                    var lookup = c as IInstanceLookup;
                    if (lookup.Parameters.Any())
                    {
                        Parameter p = lookup.Parameters.First();
                        if(p is PositionalParameter)
                        {
                            return LogManager.GetLogger((p as PositionalParameter).Value.ToString());
                        }
                    }
                }
                return LogManager.GetLogger(c.GetComponentType().FullName);
            });
            
            builder.RegisterType<TraceInterceptor>();
            builder.RegisterType<TransactionInterceptor>();

            // transactional
            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(t => t.GetCustomAttribute<DependencyObjectAttribute>()?.Transactional ?? false )
                 .AsSelf()
                 .EnableClassInterceptors()
                 .InterceptedBy(typeof(TraceInterceptor), typeof(TransactionInterceptor) )
                 .InstancePerLifetimeScope()
                 ;
            // transactional
            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .Where(t => (!t.GetCustomAttribute<DependencyObjectAttribute>()?.Transactional) ?? false)
                 .AsSelf()
                 .EnableClassInterceptors()
                 .InterceptedBy(typeof(TraceInterceptor))
                 ;
        }
    }
}
