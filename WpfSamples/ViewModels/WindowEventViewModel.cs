using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism;
using Reactive.Bindings;
using NLog;
using System.ComponentModel;

namespace WpfSamples.ViewModels
{
    public class WindowEventViewModel : BindableBase
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<CancelEventArgs> ClosingCommand { get; set; } = new ReactiveCommand<CancelEventArgs>();
        public ReactiveCommand ClosedCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand ActivatedCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand ContentRenderedCommand { get; set; } = new ReactiveCommand();
        public ReactiveProperty<bool> CancelClose { get; set; } = new ReactiveProperty<bool>();
        public WindowEventViewModel()
        {
            LoadedCommand.Subscribe(Loaded);
            ClosingCommand.Subscribe(Closing);
            ClosedCommand.Subscribe(Closed);
            ActivatedCommand.Subscribe(Activated);
            ContentRenderedCommand.Subscribe(ContentRendered);
        }
        public void Loaded()
        {
            logger.Trace("Loaded");
        }
        public void Closing(CancelEventArgs args)
        {
            logger.Trace("Closing");
            if (CancelClose.Value)
            {
                args.Cancel = true;
                logger.Trace("close canceled ");
                return;
            } 

        }
        public void Closed()
        {
            logger.Trace("Closed");

        }
        public void Activated()
        {
            logger.Trace("Activated");

        }
        public void ContentRendered()
        {
            logger.Trace("ContentRendered");

        }

    }
}
