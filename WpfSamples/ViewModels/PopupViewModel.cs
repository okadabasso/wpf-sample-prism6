using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using Reactive.Bindings;
namespace WpfSamples.ViewModels
{
    public class PopupViewModel : BindableBase, IInteractionRequestAware
    {
        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<CancelEventArgs> ClosingCommand { get; set; } = new ReactiveCommand<CancelEventArgs> ();
        public ReactiveProperty<bool> CancelClose { get; set; } = new ReactiveProperty<bool>();

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public PopupViewModel()
        {
            LoadedCommand.Subscribe(OnLoaded);
            ClosingCommand.Subscribe(OnClosing);
        }

        public void OnLoaded()
        {
        }
        public void OnClosing(CancelEventArgs args)
        {
            if (CancelClose.Value)
            {
                args.Cancel = true;
            }
        }
    }
}
