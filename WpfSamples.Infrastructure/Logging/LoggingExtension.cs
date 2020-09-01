using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace WpfSamples.Infrastructure.Logging
{
    public static class LoggingExtension
    {
        public static void BlockTrace(this ILogger logger, Action action, 
            string callerClassName = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (string.IsNullOrEmpty(callerClassName)) { callerClassName = GetCallerClassName(2); }
            NLog.LogEventInfo start = new NLog.LogEventInfo(NLog.LogLevel.Info, logger.Name, "block start");
            start.SetCallerInfo(callerClassName, callerMemberName, callerFilePath, callerLineNumber);
            logger.Log(typeof(LoggingExtension), start);

            action();

            NLog.LogEventInfo end = new NLog.LogEventInfo(NLog.LogLevel.Info, logger.Name, "block end");
            end.SetCallerInfo(callerClassName, callerMemberName, callerFilePath, callerLineNumber);
            logger.Log(typeof(LoggingExtension), end);
        }
        private static string GetCallerClassName(int skipFlames)
        {
            return new StackFrame(skipFlames).GetMethod().DeclaringType.FullName;
        }

        public static void ErrorRecursive(this ILogger logger, Exception exception)
        {
            var e = exception;
            while(e != null)
            {
                logger.Error(e);
                e = e.InnerException;
            }
        }
    }
}
