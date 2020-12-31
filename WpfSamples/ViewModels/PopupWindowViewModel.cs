using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Prism.Interactivity.InteractionRequest;
using WpfSamples.Notifications;
using Prism.Events;
using System.Windows;
using Module1.Views;

namespace WpfSamples.ViewModels
{
    public class PopupWindowViewModel : BindableBase, IInteractionRequestAware
    {
        IRegionManager _regionManager;
        private string _title;
        private string _viewName = nameof(ViewA);

        public ReactiveProperty<IRegionManager> PopupRegionManager { get; set; } = new ReactiveProperty<IRegionManager>();
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string ViewName { get => _viewName; set{ if(_viewName != value) SetProperty(ref _viewName, value); } }
        private IEventAggregator _eventAggregator;
        public PopupWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            LoadedCommand.Subscribe(OnLoaded);
            PopupRegionManager.Value = _regionManager.CreateRegionManager();

            _eventAggregator = eventAggregator;
        }

        private void OnLoaded()
        {
            if (Notification is PopupNotification n)
            {
                this.Title = n.Title;
                n.PopupRegionManager = PopupRegionManager.Value;

                PopupRegionManager.Value.RequestNavigate("PopupRegion", n.Content.ToString());

            }
            _eventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(name => {
                ViewName = name;
            });
        }
    }
}
