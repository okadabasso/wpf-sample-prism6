using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

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

            logger.LogTrace($"{callerClassName}#{callerMemberName} block start");

            action();

            logger.LogTrace($"{callerClassName}#{callerMemberName} block end");
        }
        public static async Task BlockTrace(this ILogger logger, Func<Task> func,
            string callerClassName = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (string.IsNullOrEmpty(callerClassName))
            { callerClassName = GetCallerClassName(2); }

            logger.LogTrace($"{callerClassName}#{callerMemberName} block start");

            await func();

            logger.LogTrace($"{callerClassName}#{callerMemberName} block end");
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
                logger.LogError(e,"");
                e = e.InnerException;
            }
        }
    }
}
