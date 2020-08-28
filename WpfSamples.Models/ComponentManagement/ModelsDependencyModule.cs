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

namespace WpfSamples.Models.ComponentManagement
{
    public class ModelsDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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


        }
    }
}
