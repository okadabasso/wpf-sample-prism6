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

        private int _x;
        public virtual int X
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
        public virtual int Y
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

        [Trace]
        public virtual int Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);
                RaisePropertyChanged(nameof(Result));
            }
        }
        [Trace]
        protected virtual void UpdateResult()
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
        [Trace]
        protected virtual void Change()
        {
            _logger.LogDebug("change start");
            X++;
        }
        [Trace]
        protected virtual void Close()
        {
            _logger.LogDebug("close");
            FinishInteraction();
        }

    }
}
