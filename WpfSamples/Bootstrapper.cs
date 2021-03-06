﻿using Autofac;
using Prism.Autofac;
using Prism.Modularity;
using System;
using System.Windows;

using WpfSamples.Views;

namespace WpfSamples
{
    public class Bootstrapper : AutofacBootstrapper
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Bootstrapper()
        {
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            // Shell の登録
            builder.RegisterType<MainWindow>();

            // 依存関係の注入
            builder.RegisterModule<Infrastructure.ComponentManagement.InfrastructureDependencyModule>();
            builder.RegisterModule<Models.ComponentManagement.ModelsDependencyModule>();
            builder.RegisterModule<SampleDependencyModule>();

            builder.RegisterTypeForNavigation<PopupView>();

            builder.RegisterModule<Module1.Module1DependencyModule>();
        }

        protected override void InitializeShell()
        {
            var login = new LoginWindow();
            var result = login.ShowDialog();
            if (result.HasValue && !result.Value)
            {
                Application.Current.Shutdown();
                return;
            }
            
            var window = (Window)Shell;
            window.Show();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

    }
}
