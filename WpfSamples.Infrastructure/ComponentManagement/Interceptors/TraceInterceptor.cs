using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Diagnostics;
using System.Reflection;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public class TraceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (!InterceptionEnabled(invocation)) 
            {
                invocation.Proceed();
                return;
            }
            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            this.Trace("method start", invocation, logger);
            invocation.Proceed();
            this.Trace("method end", invocation, logger);
        }
        private bool InterceptionEnabled(IInvocation invocation)
        {
            if (invocation.TargetType.GetCustomAttribute<DependencyObjectAttribute>() == null)
            {
                return false;
            }

            if (invocation.MethodInvocationTarget.GetCustomAttribute<TraceAttribute>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
