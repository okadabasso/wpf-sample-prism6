using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using Microsoft.Extensions.Logging;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public class TraceInterceptorAsync : IAsyncInterceptor
    {
        private readonly ILoggerFactory loggerFactory;

        public TraceInterceptorAsync(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            if (!InterceptEnabled(invocation))
            {
                invocation.Proceed();
                return;
            }

            var  logger = loggerFactory.CreateLogger(invocation.TargetType);
            this.Trace("method start", invocation, logger);
            invocation.Proceed();
            this.Trace($"method end with return value {invocation.ReturnValue}", invocation, logger);
        }
        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            if (!InterceptEnabled(invocation))
            {
                await Invoke(invocation);
                return;
            }

            var logger = loggerFactory.CreateLogger(invocation.TargetType);
            this.Trace("method start", invocation, logger);
            await Invoke(invocation);
            this.Trace("method end", invocation, logger);
        }


        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {

            if (!InterceptEnabled(invocation))
            {
                return await InvokeAsync<TResult>(invocation);
            }

            var logger = loggerFactory.CreateLogger(invocation.TargetType);
            this.Trace("method start", invocation, logger);
            TResult result = await InvokeAsync<TResult>(invocation);
            this.Trace($"method end with return value {result}", invocation, logger);
            return result;
        }
        private bool InterceptEnabled(IInvocation invocation)
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
        private async Task Invoke(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;
        }

        private async Task<TResult> InvokeAsync<TResult>(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;
            return result;
        }

    }
}
