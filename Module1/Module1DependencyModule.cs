using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Autofac;

namespace Module1
{
    public class Module1DependencyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterTypeForNavigation<Views.ViewA>(typeof(Views.ViewA).FullName);
            builder.RegisterTypeForNavigation<Views.ViewB>(typeof(Views.ViewB).FullName);
        }
    }
}
