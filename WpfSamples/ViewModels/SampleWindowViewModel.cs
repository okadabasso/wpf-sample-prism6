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
    public class SampleWindowViewModel : BindableBase, IInteractionRequestAware
    {
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return base.SetProperty(ref storage, value, propertyName);
        }
        protected override bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            return base.SetProperty(ref storage, value, onChanged, propertyName);

        }
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

        private int _x;
        public int X
        {
            get => _x;
            set
            {
                SetProperty(ref _x, value);
                UpdateResult();
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
            }
        }
        protected int _result;


        public int Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);
            }
        }
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
        }
        protected virtual void Change()
        {
            X++;
        }
        protected virtual void Close()
        {
            FinishInteraction();
        }

    }
}
