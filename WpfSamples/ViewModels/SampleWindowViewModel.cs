using Autofac;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace WpfSamples.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        private bool _v;
        public bool v
        {
            get { return _v; }
            set { SetProperty(ref _v, value); }
        }

        public BaseViewModel()
        {
        }
    }
    public class SampleWindowViewModel : BaseViewModel, IInteractionRequestAware
    {
        private bool _closeWindow = false;
        public bool CloseWindow
        {
            get { return _closeWindow; }
            set { SetProperty(ref _closeWindow, value); }

        }
        public DelegateCommand CloseCommand { get; set; }
        private readonly IContainer _container;
        private readonly ILogger _logger;
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand ChangeCommand { get; set; }
        private INotification _notification;
        public INotification Notification
        {
            get 
            { 
                return _notification; 
            }
            set
            {
                this._notification = value;
            }
        }

        public Action FinishInteraction { get; set; }

        public DelegateCommand LoadedAction { get; set; }

        public ReactiveProperty<bool> watcher { get; set; }
        public ReactiveProperty<int> x { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> y { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> result { get; set; } = new ReactiveProperty<int>();
        public SampleWindowViewModel (IContainer container, ILogger<SampleWindowViewModel> logger) : base()
        {
            _container = container;
            _logger = logger;
            SubmitCommand = new DelegateCommand(Close);
            LoadedAction = new DelegateCommand(OnLoaded);
            ChangeCommand = new DelegateCommand(Change);
            CloseCommand = new DelegateCommand(Close);

            v = true;
            CloseWindow = false;
            result = x.CombineLatest(y, (x, y) => x + y).ToReactiveProperty();
            watcher = this.ObserveProperty(x => x.CloseWindow).ToReactiveProperty();
            watcher.Subscribe(_ => RaisePropertyChanged("CloseWindow"));

            RaisePropertyChanged();
            RaisePropertyChanged("CloseWindow");
        }
        [Trace]
        protected virtual void OnLoaded()
        {

        }
        protected virtual void Change()
        {
            v = !v;
        }
        [Trace]
        protected virtual void Close()
        {
            CloseWindow = v;

        }

    }
    public class CloseWindowAttachedBehavior
    {
        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        // Using a DependencyProperty as the backing store for Close.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(CloseWindowAttachedBehavior), new PropertyMetadata(false, OnCloseChanged));

        private static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window win))
            {
                // Window以外のコントロールにこの添付ビヘイビアが付けられていた場合は、
                // コントロールの属しているWindowを取得
                win = Window.GetWindow(d);
            }

            if (GetClose(d))
            {
                    win.Close();
            }
        }
    }
}
