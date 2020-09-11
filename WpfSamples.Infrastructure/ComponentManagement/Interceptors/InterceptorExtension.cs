using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public static class InterceptorExtension
    {
        public static void Trace(this IInterceptor interceptor, string message, IInvocation invocation, ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.Log(LogLevel.Trace, $"{invocation.TargetType.FullName}#{invocation.MethodInvocationTarget.Name} {message}");
            }
        }
        public static void Trace(this IAsyncInterceptor interceptor, string message, IInvocation invocation, ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.Log(LogLevel.Trace, $"{invocation.TargetType.FullName}#{invocation.MethodInvocationTarget.Name} {message}");
            }
        }
    }
}
