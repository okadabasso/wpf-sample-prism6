using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.ComponentManagement.Interceptors;
using Autofac.Core;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Logging;
namespace WpfSamples.Models.ComponentManagement
{
    public class ModelsDependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<NorthwindDbContext>((c) => {
                var db = new NorthwindDbContext();
                var logger = c.Resolve<ILogger<NorthwindDbContext>>();
                db.Database.Log = sql =>
                {
                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace(sql);

                    }
                };
                return db;
            });


        }
    }
}
