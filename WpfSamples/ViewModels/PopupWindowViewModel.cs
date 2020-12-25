using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Prism.Interactivity.InteractionRequest;
using WpfSamples.Notifications;

namespace WpfSamples.ViewModels
{
    public class PopupWindowViewModel : BindableBase, IInteractionRequestAware
    {
        IRegionManager _regionManager;
        private string _title;

        public ReactiveProperty<IRegionManager> PopupRegionManager { get; set; } = new ReactiveProperty<IRegionManager>();
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public PopupWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadedCommand.Subscribe(OnLoaded);
            PopupRegionManager.Value = _regionManager.CreateRegionManager();
        }

        private void OnLoaded()
        {
            if (Notification is PopupNotification n)
            {
                this.Title = n.Title;
                n.PopupRegionManager = PopupRegionManager.Value;

                PopupRegionManager.Value.RequestNavigate("PopupRegion", n.Content.ToString());

            }

        }
    }
}
