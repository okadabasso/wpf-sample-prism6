using Autofac;
using Autofac.Core;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.Logging;
using WpfSamples.Services;
namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class SampleViewModel : BindableBase
    {
        private readonly IContainer dependencyContainer;
        private ILogger logger;
        public string Title { get; set; } = "Sample View";

        private string _message;
        public string Message 
        {
            get 
            {
                return _message;
            }
            set
            {
                SetProperty(ref _message, value);
            }
        } 

        public DelegateCommand LoadedCommand { get; set; }

        public SampleViewModel(IContainer container, ILogger logger)
        {
            this.dependencyContainer = container;
            this.logger = logger;

            logger.BlockTrace(() => {
                Foo();
                LoadedCommand = new DelegateCommand(OnLoaded);
            });

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
        [Trace]
        protected virtual void OnLoaded()
        {
            Message = "loaded " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            RaisePropertyChanged(nameof(Message));
        }
    }
}
