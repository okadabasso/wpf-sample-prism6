using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Autofac;
using System.Reflection;
namespace Module1
{
    public class Module1DependencyModule : Autofac.Module
    {
        private Action<Type, string> register;
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            var types = this.GetType().Assembly.GetTypes().Where(x => x.Namespace.EndsWith("Views"));
            foreach(var type in types)
            {
                var method = CreateRegisterViewMethod(type);
                method.Invoke(builder, new object[] {builder, type.FullName });
            }
        }
        private static MethodInfo registerMethod = typeof(AutofacExtensions).GetMethod("RegisterTypeForNavigation");
        private MethodInfo CreateRegisterViewMethod(Type viewType)
        {
            var genericMethod = registerMethod.MakeGenericMethod(viewType);
            return genericMethod;
        }
    }
}
