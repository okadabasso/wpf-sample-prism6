using Module1.Views;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfSamples.Notifications;
using WpfSamples.Views;

namespace WpfSamples.ViewModels
{
	public class PopupViewModel : BindableBase, IInteractionRequestAware
    {
        public Reactive.Bindings.ReactiveCommand LoadedCommand { get; set; } = new Reactive.Bindings.ReactiveCommand();
        public Reactive.Bindings.ReactiveProperty<IRegionManager> PopupRegionManager { get; set; } = new Reactive.Bindings.ReactiveProperty<IRegionManager>();
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public PopupViewModel(IRegionManager _regionManager)
        {
            PopupRegionManager.Value = _regionManager.CreateRegionManager();

            LoadedCommand.Subscribe(Loaded);
        }

        public void Loaded()
        {
            if (Notification is PopupNotification n)
                n.PopupRegionManager = PopupRegionManager.Value;

            PopupRegionManager.Value.RequestNavigate("PopupRegion", nameof(ViewA));
        }
	}
}
