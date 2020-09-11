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
using Microsoft.Extensions.Logging;

namespace WpfSamples.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly ILogger _logger;
        public InteractionRequest<Notification> ShowSubWindowCommand { get; set; } = new InteractionRequest<Notification>();
        public DelegateCommand<string> ShowSampleWindowCommand { get; set; }
        public MenuViewModel(ILogger<MenuViewModel> logger)
        {
            _logger = logger;
            ShowSampleWindowCommand = new DelegateCommand<string>(name => {
                this.ShowSubWindowCommand.Raise(new SubWindowOpenNotification
                {
                    ContentType =  Type.GetType(name)
                },
                notification => {
                    _logger.LogTrace($"{name} complete");
                });
            });

        }
        protected virtual void Close()
        {

        }
    }
}
