using Autofac;
using Autofac.Core;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Services;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class SampleViewModel : BindableBase
    {
        private readonly IContainer dependencyContainer;
        private ILogger logger;
        public string Title { get; set; } = "Sample View";
        public string Message { get; set; } = "hello " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        public SampleViewModel(IContainer container, ILogger logger)
        {
            this.dependencyContainer = container;
            this.logger = logger;
            logger.Trace("start");

            Foo();
        }
        [Trace]
        public virtual void Foo()
        {
            using (var scope = dependencyContainer.BeginLifetimeScope())
            {
                var service = scope.Resolve<FooService>();
                service.Send();

            }
        }
    }
}
