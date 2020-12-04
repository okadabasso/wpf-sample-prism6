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
        IAsyncInterceptor _asyncInterceptor;
        public TraceInterceptor(TraceInterceptorAsync asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }
        public void Intercept(IInvocation invocation)
        {
            if (!InterceptionEnabled(invocation))
            {
                invocation.Proceed();
                return;
            }

            _asyncInterceptor.ToInterceptor().Intercept(invocation);
        }
        private bool InterceptionEnabled(IInvocation invocation)
        {
            if (invocation.TargetType.GetCustomAttribute<DependencyObjectAttribute>() == null)
            {
                return false;
            }
            if (invocation.Method.GetCustomAttribute<TraceAttribute>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
