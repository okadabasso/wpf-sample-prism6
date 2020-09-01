using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Diagnostics;
using System.Reflection;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using System.Data.Entity;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public class TransactionInterceptor : IInterceptor
    {
        IAsyncInterceptor _asyncInterceptor;
        public TransactionInterceptor(TransactionInterceptorAsync asyncInterceptor)
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
            if (invocation.MethodInvocationTarget.GetCustomAttribute<TransactionMethodAttribute>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
