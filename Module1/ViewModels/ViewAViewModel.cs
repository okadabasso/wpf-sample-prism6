using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();

        public ReactiveCommand ShowNotifyCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<CancelEventArgs> ClosingCommand { get; set; } = new ReactiveCommand<CancelEventArgs>();

        public ReactiveProperty<bool> CancelClosing { get; set; } = new ReactiveProperty<bool>();
        public ViewAViewModel()
        {
            ShowNotifyCommand.Subscribe(() => {
                NotificationRequest.Raise(new Notification
                {
                    Title = "sample",
                    Content = "new message"
                });

            });
            ClosingCommand.Subscribe(OnClosing);
        }
        public void OnClosing(CancelEventArgs args)
        {
            if (CancelClosing.Value)
            {
                args.Cancel = true;
            }
        }
    }
}
