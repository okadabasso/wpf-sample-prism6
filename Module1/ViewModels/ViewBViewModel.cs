using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Reactive.Bindings;
using Module1.Views;
using System.ComponentModel;
using Prism.Events;

namespace Module1.ViewModels
{
    public class ViewBViewModel : BindableBase, INavigationAware
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public ReactiveCommand NavigateToViewACommand { get; set; } = new ReactiveCommand();
        IRegionNavigationService _navigationService;
        public ReactiveProperty<bool> CancelClosing { get; set; } = new ReactiveProperty<bool>();

        public ReactiveCommand ShowMessageCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<CancelEventArgs> ClosingCommand { get; set; } = new ReactiveCommand<CancelEventArgs>();

        private IEventAggregator _eventAggregator;

        public ViewBViewModel(IEventAggregator eventAggregator)
        {

            NavigateToViewACommand.Subscribe(NavigateToViewA);
            ClosingCommand.Subscribe(OnClosing);

            _eventAggregator = eventAggregator;
        }
        public void OnClosing(CancelEventArgs args)
        {
            if (CancelClosing.Value)
            {
                args.Cancel = true;
            }
        }
        private void NavigateToViewA()
        {
            _navigationService.RequestNavigate(typeof(ViewA).FullName);
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            logger.Trace("IsNavigationTarget");
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            logger.Trace("OnNavigatedFrom");
            CancelClosing.Value = false;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            logger.Trace("OnNavigatedTo");
            _navigationService = navigationContext.NavigationService;

            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish(nameof(ViewB));
        }
    }
}
