using Autofac;
using NLog;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.Logging;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class SubWindowViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;
        public DelegateCommand SubmitCommand { get; set; }
        private INotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set
            {
                this._notification = value;
            }
        }

        public Action FinishInteraction { get; set; }

        public DelegateCommand LoadedAction { get; set; }
        public SubWindowViewModel (IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
            _logger.BlockTrace(() => {
                SubmitCommand = new DelegateCommand(Close);
                LoadedAction = new DelegateCommand(OnLoaded);
            });
        }
        [Trace]
        protected virtual void OnLoaded()
        {

        }
        [Trace]
        protected virtual void Close()
        {
            FinishInteraction();

        }

    }
}
