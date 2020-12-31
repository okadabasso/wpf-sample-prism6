using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSamples
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetExceptionHandlers();

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
        /// <summary>
        /// 未処理例外の処理設定を行う
        /// </summary>
        private void SetExceptionHandlers()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// UI スレッドで発生した未処理例外を処理する
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error(e.Exception);
            Environment.Exit(1);
        }

        /// <summary>
        /// バックグラウンドタスクで発生した未処理例外を処理する
        /// </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            logger.Error(e.Exception.InnerException);
            Environment.Exit(1);
        }

        
        /// <summary>
        /// 最終的に処理されなかった未処理例外を処理する
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error(e.ExceptionObject);
            Environment.Exit(1);
        }

    }
}
