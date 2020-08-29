using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Notifications;
using WpfSamples.Views;
using Prism.Interactivity.InteractionRequest;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class MenuViewModel : BindableBase
    {
        private readonly ILogger _logger;
        public InteractionRequest<Notification> ShowSubWindowCommand { get; set; } = new InteractionRequest<Notification>();
        public DelegateCommand<string> ShowSampleWindowCommand { get; set; }
        public MenuViewModel(ILogger logger)
        {
            _logger = logger;
            ShowSampleWindowCommand = new DelegateCommand<string>(name => {
                this.ShowSubWindowCommand.Raise(new SubWindowOpenNotification
                {
                    ContentType =  Type.GetType(name)
                },
                notification => {
                    _logger.Trace($"{name} complete");
                });
            });

        }
    }
}
