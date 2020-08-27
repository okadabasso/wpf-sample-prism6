using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public static class InterceptorExtension
    {
        public static void Trace(this IInterceptor interceptor, string message, IInvocation invocation, ILogger logger)
        {
            if (logger.IsTraceEnabled)
            {
                var callerClassName = invocation.TargetType.FullName;
                NLog.LogEventInfo info = new NLog.LogEventInfo(NLog.LogLevel.Trace, logger.Name, message);
                // 呼び出し元情報を設定します。
                info.SetCallerInfo(callerClassName, invocation.MethodInvocationTarget.Name, null, 0);
                logger.Log(typeof(TraceInterceptor), info);

            }
        }
    }
}
