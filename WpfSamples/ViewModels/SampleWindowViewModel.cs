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
using System.ComponentModel;

namespace WpfSamples.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel()
        {
        }
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] String propertyName = "")
        {
            field = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    [DependencyObject]
    public class SampleWindowViewModel : BindableBase, IInteractionRequestAware
    {

        private readonly Autofac.IContainer _container;
        private readonly ILogger _logger;

        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand ChangeCommand { get; set; }
        private INotification _notification;
        public INotification Notification
        {
            get =>  _notification; 
            set => this._notification = value;
            
        }
        public Action FinishInteraction { get; set; }

        protected int _x;
        public int X
        {
            get => _x;
            set
            {
                SetProperty(ref _x, value);
                UpdateResult();
                RaisePropertyChanged(nameof(X));
            }
        }
        protected int _y;
        public int Y
        {
            get => _y;
            set
            {
                SetProperty(ref _y, value);
                UpdateResult();
                RaisePropertyChanged(nameof(Y));
            }
        }
        protected int _result;


        public int Result { get => _result; set => SetProperty(ref _result, value); }
        private void UpdateResult()
        {
            Result = X + Y;
        }

        public SampleWindowViewModel (Autofac.IContainer container, ILogger<SampleWindowViewModel> logger) : base()
        {
            _container = container;
            _logger = logger;
            SubmitCommand = new DelegateCommand(Close);
            ChangeCommand = new DelegateCommand(Change);
            CloseCommand = new DelegateCommand(Close);
            RaisePropertyChanged(null);
        }
        [Trace]
        protected virtual void Change()
        {
            X++;
        }
        [Trace]
        protected virtual void Close()
        {
            FinishInteraction();

        }

    }
}
