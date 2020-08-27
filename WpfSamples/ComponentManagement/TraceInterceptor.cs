using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Diagnostics;
using System.Reflection;
using WpfSamples.ComponentManagement.Attributes;
namespace WpfSamples.ComponentManagement
{
    public class TraceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var traceEnabled = TraceEnabled(invocation);
            if (traceEnabled) 
            { 
                var logger = LogManager.GetLogger(invocation.TargetType.FullName);
                Trace(logger, "method start", invocation);
                invocation.Proceed();
                Trace(logger, "method end", invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }
        private bool TraceEnabled(IInvocation invocation)
        {
            if (invocation.MethodInvocationTarget.GetCustomAttribute<TraceAttribute>() == null)
            {
                return false;
            }
            return true;
        }
        private void Trace(ILogger logger, string message, IInvocation invocation)
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
