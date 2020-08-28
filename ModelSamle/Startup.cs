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

            builder.Register<NorthwindDbContext>((c) => {
                var db = new NorthwindDbContext();
                var parameter = new PositionalParameter(0, typeof(NorthwindDbContext).FullName);
                var logger = c.Resolve<ILogger>(parameter);
                db.Database.Log = sql =>
                {
                    if (logger.IsTraceEnabled)
                    {
                        var callerClassName = typeof(NorthwindDbContext).FullName;
                        NLog.LogEventInfo info = new NLog.LogEventInfo(NLog.LogLevel.Trace, logger.Name, sql);
                        // 呼び出し元情報を設定します。
                        info.SetCallerInfo(callerClassName, null, null, 0);
                        logger.Log(typeof(ModelsDependencyModule), info);

                    }
                };
                return db;
            });



            return builder.Build();
        }
    }
}
