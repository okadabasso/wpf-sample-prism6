using Autofac;
using Autofac.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Models;
using NLog;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.ComponentManagement.Interceptors;
using Castle.Components.DictionaryAdapter.Xml;
using Autofac.Core.Resolving;

namespace ModelSamle
{
    public class Startup
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register<ILogger>((c) => {
                if (c is IInstanceLookup)
                {
                    var lookup = c as IInstanceLookup;
                    if (lookup.Parameters.Any())
                    {
                        Parameter p = lookup.Parameters.First();
                        if (p is PositionalParameter)
                        {
                            return LogManager.GetLogger((p as PositionalParameter).Value.ToString());
                        }
                    }
                }
                return LogManager.GetLogger(c.GetComponentType().FullName);
            });

            builder.RegisterType<TraceInterceptor>();
            builder.RegisterType<TransactionInterceptor>();

            builder.RegisterModule<ModelsDependencyModule>();




            return builder.Build();
        }
    }
}
