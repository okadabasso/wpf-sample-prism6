using Module1.Views;
using NLog;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Module1.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();

        public ReactiveProperty<bool> CancelClosing { get; set; } = new ReactiveProperty<bool>();

        public ReactiveCommand ShowMessageCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<CancelEventArgs> ClosingCommand { get; set; } = new ReactiveCommand<CancelEventArgs>();
        public ReactiveCommand NavigateToViewBCommand { get; set; } = new ReactiveCommand();

        IRegionNavigationService _navigationService;


        public ViewAViewModel()
        {
            ShowMessageCommand.Subscribe(() => {
                NotificationRequest.Raise(new Notification
                {
                    Title = "sample",
                    Content = "new message"
                });

            });
            ClosingCommand.Subscribe(OnClosing);
            NavigateToViewBCommand.Subscribe(NavigateToViewB);
        }
        public void OnClosing(CancelEventArgs args)
        {
            if (CancelClosing.Value)
            {
                if (MessageBox.Show("Do you want to close?", "Navigate", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    args.Cancel = true;
                }
            }
        }
        private void NavigateToViewB()
        {
            _navigationService.RequestNavigate(typeof(ViewB).FullName);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            logger.Trace("IsNavigationTarget");
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            logger.Trace("OnNavigatedFrom");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;
            logger.Trace("OnNavigatedTo");
        }
    }
}
