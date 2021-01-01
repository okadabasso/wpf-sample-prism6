using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Views;
using Prism.Interactivity.InteractionRequest;
using Microsoft.Extensions.Logging;
using Module1.Views;
using WpfSamples.Infrastructure.Presentation.Notifications;

namespace WpfSamples.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly ILogger _logger;
        public DelegateCommand ShowPopupWindowCommand { get; set; }
        public DelegateCommand<string> ShowSampleWindowCommand { get; set; }
        public DelegateCommand<string> ShowCustomPopupWindowCommand { get; set; }
        public InteractionRequest<Notification> ShowPopupRequest { get; set; } = new InteractionRequest<Notification>();
        public InteractionRequest<Notification> ShowSubWindowRequest { get; set; } = new InteractionRequest<Notification>();
        public InteractionRequest<Notification> ShowCustomPopupWindowRequest { get; set; } = new InteractionRequest<Notification>();


        public MenuViewModel(ILogger<MenuViewModel> logger)
        {
            _logger = logger;
            ShowSampleWindowCommand = new DelegateCommand<string>(name => {
                this.ShowSubWindowRequest.Raise(new Infrastructure.Presentation.Notifications.SubWindowOpenNotification
                {
                    ContentType =  Type.GetType(name)
                },
                notification => {
                    _logger.LogTrace($"{name} complete");
                });
            });
            ShowPopupWindowCommand = new DelegateCommand(() => {
                this.ShowPopupRequest.Raise(new PopupNotification
                {
                   Title = "test",
                    Content = new PopupView()  
                },
                notification => {
                    _logger.LogTrace($"subwindow complete");
                });
            });
            ShowCustomPopupWindowCommand = new DelegateCommand<string>(name => {
                this.ShowCustomPopupWindowRequest.Raise(new PopupNotification
                {
                    Title = "test",
                    Content = name
                },
                notification => {
                    _logger.LogTrace($"subwindow complete");
                });
            });

        }
        protected virtual void Close()
        {

        }
    }
}
